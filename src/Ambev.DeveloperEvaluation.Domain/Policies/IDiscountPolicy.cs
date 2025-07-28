using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Policies;

public interface IDiscountPolicy
{
    decimal Apply(SaleItem item);
}
