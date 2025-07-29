using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class QuantitySaleItemValidator : AbstractValidator<int>
{
    public QuantitySaleItemValidator()
    {
        RuleFor(item => item).Must(v => {
            var spec = new SaleItemQuantityWithinRangeSpecification();
            return spec.IsSatisfiedBy(v);
        });
    }
}
