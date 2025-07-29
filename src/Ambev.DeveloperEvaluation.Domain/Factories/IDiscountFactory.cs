using Ambev.DeveloperEvaluation.Domain.Policies;

namespace Ambev.DeveloperEvaluation.Domain.Factories
{
    public interface IDiscountFactory
    {
        IDiscountPolicy CreateForQuantity(int quantity);
    }
}