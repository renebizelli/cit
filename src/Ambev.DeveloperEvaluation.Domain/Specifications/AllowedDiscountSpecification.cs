using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface IAllowedDiscountSpecification : ISpecification<Sale>;

public class AllowedDiscountSpecification : IAllowedDiscountSpecification
{
    public bool IsSatisfiedBy(Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale, nameof(sale));

        return sale.ActiveItems().Count() > 4;
    }
}
