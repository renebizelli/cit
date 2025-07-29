using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Messages;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Application.Sales._Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly ISalePricingService _salePricingService;
    private readonly IMessageSender _messageSender;
    private readonly IStringIDGenerator _stringIDGenerator;
    private readonly ILongIDGenerator _longIDGenerator;
    private readonly IMinimumCartItemSpecification _minimumCartItem;
    private readonly ISaleItemQuantityWithinRangeSpecification _saleItemQuantityWithinRange;

    public SaleService(
        ISaleRepository saleRepository,
        ICartService cartService,
        IProductService productService,
        ISalePricingService salePricingService,
        IMessageSender messageSender,
        IMinimumCartItemSpecification minimumCartItem,
        ISaleItemQuantityWithinRangeSpecification saleItemQuantityWithinRange,
        IStringIDGenerator stringIDGenerator,
        ILongIDGenerator longIDGenerator)
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
    }

    public async Task<Sale> CreateAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default)
    {
        var cart = await GetCartByUserForSaleCreatingAsync(userBranchKey, cancellationToken);

        if (!_minimumCartItem.IsSatisfiedBy(cart)) throw new DomainException("##TODO: Invalid quantity of items cart");

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

        if (cart == null) throw new InvalidOperationException("##TODO: Cart not found for sale creation");

        return cart;
    }

    private void SaleItemQuantityWithinRange(Cart cart)
    {
        foreach (var item in cart.Items)
        {
            if (!_saleItemQuantityWithinRange.IsSatisfiedBy(item.Quantity)) throw new DomainException("##TODO: Invalid quantity of item");
        }
    }

    private async Task<Sale> SaleMappingAsync(Cart cart, CancellationToken cancellationToken)
    {
        var saleNumber = await _longIDGenerator.GenerateSaleNumber(cancellationToken);
        if (saleNumber <= 0) throw new DomainException("##TODO: Invalid sale number generated");

        var sale = new Sale(
            _stringIDGenerator.Generate(),
            saleNumber,
            new Sale.SaleBranch(cart.BranchId, "TODO"),
            new Sale.SaleUser(cart.UserId, "TODO", "TODO", "TODO"));

        foreach (var item in cart.Items)
        {
            var product = await _productService.GetAsync(item.ProductId, cancellationToken);
            if (product == null) throw new InvalidOperationException($"##TODO: Product with ID {item.ProductId} not found.");

            var saleItem = new Sale.SaleItem(
                _stringIDGenerator.Generate(),
                new Sale.SaleProduct(product.Id, product.Title, product.Price),
                item.Quantity);

            sale.AddItem(saleItem);

        }

        return sale;
    }
}
