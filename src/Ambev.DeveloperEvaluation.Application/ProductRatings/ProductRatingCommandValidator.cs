using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.ProductRatings;

public class ProductRatingCommandValidator : AbstractValidator<ProductRatingCommand>
{
    public ProductRatingCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
        RuleFor(x => x.Rating)
            .SetValidator(new ProductRatingValidator())
            .WithMessage("Rating must be between 1 and 5.");
    }
}
