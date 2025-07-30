using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductRatingValidator : AbstractValidator<int>
{
    public ProductRatingValidator()
    {
        RuleFor(x => x).InclusiveBetween(1, 5);
    }
}
