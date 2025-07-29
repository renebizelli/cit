using Ambev.DeveloperEvaluation.Application.Branches._Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Messages;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Sales._Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly ISalePricingService _salePricingService;
    private readonly IMessageSender _messageSender;
    private readonly IStringIDGenerator _stringIDGenerator;
    private readonly ILongIDGenerator _longIDGenerator;
    private readonly IMinimumCartItemSpecification _minimumCartItem;
    private readonly ISaleItemQuantityWithinRangeSpecification _saleItemQuantityWithinRange;
    private readonly IBranchService _branchService;

    public SaleService(
        ISaleRepository saleRepository,
        ICartService cartService,
        IProductService productService,
        ISalePricingService salePricingService,
        IMessageSender messageSender,
        IMinimumCartItemSpecification minimumCartItem,
        ISaleItemQuantityWithinRangeSpecification saleItemQuantityWithinRange,
        IStringIDGenerator stringIDGenerator,
        ILongIDGenerator longIDGenerator,
        IUserService userService,
        IBranchService branchService)
    {
        _saleRepository = saleRepository;
        _cartService = cartService;
        _productService = productService;
        _salePricingService = salePricingService;
        _messageSender = messageSender;
        _minimumCartItem = minimumCartItem;
        _saleItemQuantityWithinRange = saleItemQuantityWithinRange;
        _stringIDGenerator = stringIDGenerator;
        _longIDGenerator = longIDGenerator;
        _userService = userService;
        _branchService = branchService;
    }

    public async Task<Sale> CreateAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByUserForSaleCreatingAsync(userBranchKey, cancellationToken);

        if (!_minimumCartItem.IsSatisfiedBy(cart)) throw new DomainException("Invalid quantity of items cart");

        SaleItemQuantityWithinRange(cart);

        var sale = await SaleMappingAsync(cart, cancellationToken);

        _salePricingService.ApplyDiscounts(sale);   

        sale = await _saleRepository.CreateAsync(sale, cancellationToken);

        await _messageSender.SendAsync(new SaleCreated(sale));

        await _cartService.DeleteAsync(userBranchKey, cancellationToken);

        return sale;
    }

    private async Task<Cart> GetCartByUserForSaleCreatingAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetByUserForSaleCreatingAsync(userBranchKey, cancellationToken);

        if (cart == null) throw new InvalidOperationException("Cart not found for sale creation");

        return cart;
    }

    private void SaleItemQuantityWithinRange(Cart cart)
    {
        foreach (var item in cart.Items)
        {
            if (!_saleItemQuantityWithinRange.IsSatisfiedBy(item.Quantity)) throw new DomainException("Invalid quantity of item");
        }
    }

    private async Task<Sale> SaleMappingAsync(Cart cart, CancellationToken cancellationToken)
    {
        var saleNumber = await _longIDGenerator.GenerateSaleNumber(cancellationToken);
        if (saleNumber <= 0) throw new DomainException("Invalid sale number generated");

        var user = await _userService.GetByIdAsync(cart.UserId, cancellationToken);
        if (user == null) throw new DomainException("Invalid user");

        var branch = await _branchService.GetAsync(cart.BranchId, cancellationToken);
        if (branch == null) throw new DomainException("Invalid branch");

        var sale = new Sale(
            _stringIDGenerator.Generate(),
            saleNumber,
            new Sale.SaleBranch(cart.BranchId, branch.Name),
            new Sale.SaleUser(cart.UserId, user.Username, user.Address?.City, user.Address?.State));

        foreach (var item in cart.Items)
        {
            var product = await _productService.GetAsync(item.ProductId, cancellationToken);
            if (product == null) throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

            var saleItem = new Sale.SaleItem(
                _stringIDGenerator.Generate(),
                new Sale.SaleProduct(product.Id, product.Title, product.Price),
                item.Quantity);

            sale.AddItem(saleItem);

        }

        return sale;
    }

    public async Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken = default)
    {
        var sale = await _saleRepository.GetAsync(id, saleNumber, cancellationToken);
        if (sale == null) throw new DomainException("Sale not found");

        sale.AttachEvents();

        return sale;
    }

    public async Task CancelSaleItemAsync(string saleId, string saleItemId, CancellationToken cancellationToken = default)
    {
        var sale = await GetAsync(saleId, 0, cancellationToken);
        if (sale == null) throw new DomainException("Sale not found");

        var item = sale.Items.FirstOrDefault(f => f.Id == saleItemId);   
        if (item == null) throw new DomainException("Sale Item not found");

        item.Cancel();

        _salePricingService.ApplyDiscounts(sale);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _messageSender.SendAsync(new SaleItemCancelled(saleId, saleItemId));
    }

    public async Task CancelAsync(string saleId, CancellationToken cancellationToken = default)
    {
        var result = await _saleRepository.CancelAsync(saleId, cancellationToken);
        if (result == 0) throw new DomainException("Sale not found");

        await _messageSender.SendAsync(new SaleCancelled(saleId));
    }

    public async Task<(long, IList<Sale>)> ListSalesAsync(ISalesQuerySettings querySettings, CancellationToken cancellationToken)
    {
        var allowedOrderFields = new Dictionary<string, Expression<Func<Sale, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["saleNumber"] = p => p.SaleNumber,
            ["branch"] = p => p.Branch.Name,
            ["user"] = p => p.User.Name,
            ["createdAt"] = p => p.CreatedAt,
            ["totalAmount"] = p => p.TotalAmount,
        };

        return await _saleRepository.ListSalesAsync(
            querySettings,
            allowedOrderFields,
            cancellationToken);
    }
}
