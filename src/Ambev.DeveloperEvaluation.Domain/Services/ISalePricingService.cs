using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISalePricingService
{
    void ApplyDiscounts(Sale sale);
}
