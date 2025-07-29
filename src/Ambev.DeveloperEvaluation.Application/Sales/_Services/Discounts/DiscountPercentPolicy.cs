using Ambev.DeveloperEvaluation.Domain.Policies;
using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public class DiscountPercentPolicy(decimal percentage) : IDiscountPolicy
{
    private readonly decimal percentage = percentage;

    public decimal Apply(SaleItem item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        return (item.Product.Price * item.Quantity) * percentage;
    }
}
