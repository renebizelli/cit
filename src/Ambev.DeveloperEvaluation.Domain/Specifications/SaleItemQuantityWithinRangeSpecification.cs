namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface ISaleItemQuantityWithinRangeSpecification : ISpecification<int>;

public class SaleItemQuantityWithinRangeSpecification : ISaleItemQuantityWithinRangeSpecification
{
    public bool IsSatisfiedBy(int quantity)
    {
        return quantity > 0 && quantity <= 20;
    }
}
