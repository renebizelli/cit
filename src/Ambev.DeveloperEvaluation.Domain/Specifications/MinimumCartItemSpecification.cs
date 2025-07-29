using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface IMinimumCartItemSpecification : ISpecification<Cart>;

public class MinimumCartItemSpecification : IMinimumCartItemSpecification
{
    public bool IsSatisfiedBy(Cart entity)
    => entity.Items.Any();
}
