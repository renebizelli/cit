using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Application.Sales._Services;

public class SalePricingService : ISalePricingService
{
    private readonly IDiscountFactory _discountFactory;
    private readonly IAllowedDiscountSpecification _allowedDiscountSpecification;

    public SalePricingService(
        IDiscountFactory discountFactory,
        IAllowedDiscountSpecification allowedDiscountSpecification)
    {
        _discountFactory = discountFactory;
        _allowedDiscountSpecification = allowedDiscountSpecification;
    }

    public void ApplyDiscounts(Sale sale)
    {
        sale.ResetDiscounts();

        if (!_allowedDiscountSpecification.IsSatisfiedBy(sale)) return;

        foreach (var item in sale.ActiveItems())
        {
            var discountPolicy = _discountFactory.CreateForQuantity(item.Quantity);
            item.ApplyDiscount(discountPolicy);
        }
    }
}
