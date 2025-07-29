using Ambev.DeveloperEvaluation.Application.Carts._Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using FluentAssertions;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts._Shared;

public class CartServiceTests
{
    private readonly ICacheRepository _cache = Substitute.For<ICacheRepository>();
    private readonly ICartRepository _repository = Substitute.For<ICartRepository>();
    private readonly ILogger<CartService> _logger = Substitute.For<ILogger<CartService>>();
    private readonly CartService _service;

    public CartServiceTests()
    {
        _service = new CartService(_cache, _repository, _logger);
    }

    private static Cart CreateCart(Guid userId, Guid branchId, int itemCount = 1)
    {
        var cart = new Cart { UserId = userId, BranchId = branchId, Items = new List<CartItem>() };
        for (int i = 0; i < itemCount; i++)
            cart.Items.Add(new CartItem { ProductId = $"prod{i}", Quantity = 1 });
        return cart;
    }

    [Fact]
    public async Task CreateOrUpdateAsync_CallsRepositoryAndCache()
    {
        var cart = CreateCart(Guid.NewGuid(), Guid.NewGuid(), 2);

        await _service.CreateOrUpdateAsync(cart);

        await _repository.Received(1).CreateOrUpdateCartAsync(cart, Arg.Any<CancellationToken>());
        await _cache.Received(1).SetAsync(Arg.Any<CartCacheSetOptions>(), cart);
    }

    [Fact]
    public async Task DeleteAsync_CallsCacheAndRepository()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        _repository.DeleteCartAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns(1);

        await _service.DeleteAsync(userBranchKey);

        await _cache.Received(1).DeleteAsync(Arg.Any<CartCacheDeleteOptions>());
        await _repository.Received(1).DeleteCartAsync(userBranchKey, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ThrowsKeyNotFoundException_WhenRepositoryReturnsZero()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        _repository.DeleteCartAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns(0);

        var act = async () => await _service.DeleteAsync(userBranchKey);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GetByUserForSaleCreatingAsync_ReturnsCartFromRepository()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        var cart = CreateCart(userBranchKey.UserId, userBranchKey.BranchId);
        _repository.GetCartByUserAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns(cart);

        var result = await _service.GetByUserForSaleCreatingAsync(userBranchKey);

        result.Should().Be(cart);
    }

    [Fact]
    public async Task GetByUserAsync_ReturnsCartFromCache_IfExists()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        var cart = CreateCart(userBranchKey.UserId, userBranchKey.BranchId);
        _cache.GetAsync<Cart>(Arg.Any<CartCacheGetOptions>()).Returns(cart);

        var result = await _service.GetByUserAsync(userBranchKey);

        result.Should().Be(cart);
        await _repository.DidNotReceive().GetCartByUserAsync(userBranchKey, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetByUserAsync_ReturnsCartFromRepository_IfNotInCache()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        var cart = CreateCart(userBranchKey.UserId, userBranchKey.BranchId);
        _cache.GetAsync<Cart>(Arg.Any<CartCacheGetOptions>()).Returns((Cart)null);
        _repository.GetCartByUserAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns(cart);

        var result = await _service.GetByUserAsync(userBranchKey);

        result.Should().Be(cart);
        await _repository.Received(1).GetCartByUserAsync(userBranchKey, Arg.Any<CancellationToken>());
        await _cache.Received(1).SetAsync(Arg.Any<CartCacheSetOptions>(), cart);
    }

    [Fact]
    public async Task GetByUserAsync_CreatesNewCart_IfNotInCacheOrRepository()
    {
        var userBranchKey = Substitute.For<IUserBranchKey>();
        _cache.GetAsync<Cart>(Arg.Any<CartCacheGetOptions>()).Returns((Cart)null);
        _repository.GetCartByUserAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns((Cart)null);

        var result = await _service.GetByUserAsync(userBranchKey);

        result.Should().NotBeNull();
        await _repository.Received(1).CreateOrUpdateCartAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        await _cache.Received(1).SetAsync(Arg.Any<CartCacheSetOptions>(), Arg.Any<Cart>());
    }
}