using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Sales._Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Messages;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class SaleServiceTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly ICartService _cartService = Substitute.For<ICartService>();
    private readonly IProductService _productService = Substitute.For<IProductService>();
    private readonly ISalePricingService _salePricingService = Substitute.For<ISalePricingService>();
    private readonly IMessageSender _messageSender = Substitute.For<IMessageSender>();
    private readonly IStringIDGenerator _stringIDGenerator = Substitute.For<IStringIDGenerator>();
    private readonly ILongIDGenerator _longIDGenerator = Substitute.For<ILongIDGenerator>();
    private readonly IMinimumCartItemSpecification _minimumCartItem = Substitute.For<IMinimumCartItemSpecification>();
    private readonly ISaleItemQuantityWithinRangeSpecification _saleItemQuantityWithinRange = Substitute.For<ISaleItemQuantityWithinRangeSpecification>();
    private readonly IBranchService _branchService  = Substitute.For<IBranchService>();
    private readonly IUserService _userService = Substitute.For<IUserService>();

    private readonly SaleService _service;

    public SaleServiceTests()
    {
        _service = new SaleService(
            _saleRepository,
            _cartService,
            _productService,
            _salePricingService,
            _messageSender,
            _minimumCartItem,
            _saleItemQuantityWithinRange,
            _stringIDGenerator,
            _longIDGenerator,
            _userService,
            _branchService
        );
    }

    [Fact]
    public async Task CreateAsync_ValidCart_CreatesSaleAndDeletesCart()
    {
        var userBranchKey = new Faker<UserBranchKey>()
            .RuleFor(u => u.UserId, f => Guid.NewGuid())
            .RuleFor(u => u.BranchId, f => Guid.NewGuid())
            .Generate();

        var cart = new Cart(userBranchKey)
        {
            Items = new List<CartItem> { new CartItem { ProductId = "prod1", Quantity = 2 } }
        };

        var user = new User {
            Id = userBranchKey.UserId,
            Username = "User",
            Address = new Address { City = "City", State = "State" }
        };

        var branch = new Branch
        {
            Id = userBranchKey.BranchId,
            Name = "Branch",
        };

        var product = new Product { Id = "prod1", Title = "Beer", Price = 10m };
        var sale = new Sale("saleId", 123, new Sale.SaleBranch(userBranchKey.BranchId, "Branch"), new Sale.SaleUser(user.Id, user.Username, user.Address.City, user.Address.State));

        _cartService.GetByUserForSaleCreatingAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns(cart);
        _minimumCartItem.IsSatisfiedBy(cart).Returns(true);
        _saleItemQuantityWithinRange.IsSatisfiedBy(2).Returns(true);
        _longIDGenerator.GenerateSaleNumber(Arg.Any<CancellationToken>()).Returns(123);
        _stringIDGenerator.Generate().Returns("saleId", "itemId");
        _productService.GetAsync("prod1", Arg.Any<CancellationToken>()).Returns(product);
        _userService.GetByIdAsync(userBranchKey.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _branchService.GetAsync(userBranchKey.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        var result = await _service.CreateAsync(userBranchKey, CancellationToken.None);

        result.Should().NotBeNull();
        await _cartService.Received(1).DeleteAsync(userBranchKey, Arg.Any<CancellationToken>());
        await _messageSender.Received(1).SendAsync(Arg.Any<SaleCreated>());
    }

    [Fact]
    public async Task CreateAsync_CartNotFound_ThrowsException()
    {
        var userBranchKey = new UserBranchKey(Guid.NewGuid(), Guid.NewGuid());
        _cartService.GetByUserForSaleCreatingAsync(userBranchKey, Arg.Any<CancellationToken>()).Returns((Cart)null);

        Func<Task> act = async () => await _service.CreateAsync(userBranchKey, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetAsync_SaleExists_ReturnsSale()
    {
        var sale = new Sale("saleId", 123, new Sale.SaleBranch(Guid.NewGuid(), "Branch"), new Sale.SaleUser(Guid.NewGuid(), "User", "City", "State"));
        _saleRepository.GetAsync("saleId", 123, Arg.Any<CancellationToken>()).Returns(sale);

        var result = await _service.GetAsync("saleId", 123, CancellationToken.None);

        result.Should().Be(sale);
    }

    [Fact]
    public async Task GetAsync_SaleNotFound_ThrowsDomainException()
    {
        _saleRepository.GetAsync("saleId", 123, Arg.Any<CancellationToken>()).Returns((Sale)null);

        Func<Task> act = async () => await _service.GetAsync("saleId", 123, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task CancelSaleItemAsync_ItemExists_CancelsItemAndUpdatesSale()
    {
        var sale = new Sale("saleId", 123, new Sale.SaleBranch(Guid.NewGuid(), "Branch"), new Sale.SaleUser(Guid.NewGuid(), "User", "City", "State"));
        var item = new Sale.SaleItem("itemId", new Sale.SaleProduct("prod1", "Beer", 10m), 2);
        sale.Items.Add(item);

        _saleRepository.GetAsync("saleId", 0, Arg.Any<CancellationToken>()).Returns(sale);

        await _service.CancelSaleItemAsync("saleId", "itemId", CancellationToken.None);

        item.Status.Should().Be(SaleItemStatus.Canceled);
        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        await _messageSender.Received(1).SendAsync(Arg.Any<SaleItemCancelled>());
    }

    [Fact]
    public async Task CancelAsync_SaleExists_CancelsSale()
    {
        _saleRepository.CancelAsync("saleId", Arg.Any<CancellationToken>()).Returns(1);

        await _service.CancelAsync("saleId", CancellationToken.None);

        await _messageSender.Received(1).SendAsync(Arg.Any<SaleCancelled>());
    }

    [Fact]
    public async Task CancelAsync_SaleNotFound_ThrowsDomainException()
    {
        _saleRepository.CancelAsync("saleId", Arg.Any<CancellationToken>()).Returns(0);

        Func<Task> act = async () => await _service.CancelAsync("saleId", CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task ListSalesAsync_ReturnsSalesList()
    {
        var querySettings = Substitute.For<ISalesQuerySettings>();
        var sales = new List<Sale> { new Sale("saleId", 123, new Sale.SaleBranch(Guid.NewGuid(), "Branch"), new Sale.SaleUser(Guid.NewGuid(), "User", "City", "State")) };
        _saleRepository.ListSalesAsync(querySettings, Arg.Any<Dictionary<string, System.Linq.Expressions.Expression<Func<Sale, object>>>>(), Arg.Any<CancellationToken>())
            .Returns((1, sales));

        var (count, result) = await _service.ListSalesAsync(querySettings, CancellationToken.None);

        count.Should().Be(1);
        result.Should().BeEquivalentTo(sales);
    }
}