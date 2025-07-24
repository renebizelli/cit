namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class QuantitySaleItemSpecification : ISpecification<int>
{
    public bool IsSatisfiedBy(int quantity)
    {
        return quantity > 0 && quantity <= 20;
    }
}
