using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Policies;

namespace Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;

public class DiscountFactory : IDiscountFactory
{
    public IDiscountPolicy CreateForQuantity(int quantity)
        => quantity switch
        {
            >= 4 and <= 9 => new DiscountPercentPolicy(0.1M),
            >= 10 and <= 20 => new DiscountPercentPolicy(0.2M),
            _ => new DiscountPercentPolicy(0M)
        };

}
