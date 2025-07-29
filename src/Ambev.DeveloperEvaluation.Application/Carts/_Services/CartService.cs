using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts._Services;

public class CartService : ICartService
{
    private readonly ICacheRepository _cache;
    private readonly ICartRepository _repository;
    private readonly ILogger<CartService> _logger;
    public CartService(
    ICacheRepository cache,
    ICartRepository cart,
    ILogger<CartService> logger)
    {
        _repository = cart;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Cart> CreateOrUpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _repository.CreateOrUpdateCartAsync(cart, cancellationToken);

        await CachingAsync(cart, cart);

        return cart;
    }

    public async Task DeleteAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default)
    {
        var cartCacheDeleteOptions = new CartCacheDeleteOptions(userBranchKey);

        await _cache.DeleteAsync(cartCacheDeleteOptions);

        var count = await _repository.DeleteCartAsync(userBranchKey, cancellationToken);

        if (count.Equals(0)) throw new KeyNotFoundException($"Product not found");
    }

    public async Task<Cart?> GetByUserForSaleCreatingAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default)
    => await _repository.GetCartByUserAsync(userBranchKey, cancellationToken);

    public async Task<Cart> GetByUserAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default)
    {
        var cacheGetOptions = new CartCacheGetOptions(userBranchKey);

        var cart = await _cache.GetAsync<Cart>(cacheGetOptions);

        if (cart != null) return cart;

        cart = await _repository.GetCartByUserAsync(userBranchKey, cancellationToken);

        if (cart == null)
        {
            cart = new Cart(userBranchKey);
            await _repository.CreateOrUpdateCartAsync(cart, cancellationToken);
        }

        await CachingAsync(userBranchKey, cart);

        return cart;
    }

    private async Task CachingAsync(IUserBranchKey userBranchKey, Cart cart)
    {
        var cartCacheSetOptions = new CartCacheSetOptions(userBranchKey, TimeSpan.FromSeconds(120));
        await _cache.SetAsync(cartCacheSetOptions, cart);
    }
}
