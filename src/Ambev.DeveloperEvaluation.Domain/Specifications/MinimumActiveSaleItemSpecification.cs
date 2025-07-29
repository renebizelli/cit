using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface IMinimumActiveSaleItemSpecification : ISpecification<Sale>;

public class MinimumActiveSaleItemSpecification : IMinimumActiveSaleItemSpecification
{
    public bool IsSatisfiedBy(Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale, nameof(sale));

        return sale.ActiveItems().Any();
    }
}
