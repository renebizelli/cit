using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Rating
{
    public class ProductRatingRequestValidator : AbstractValidator<ProductRatingRequest>
    {
        public ProductRatingRequestValidator()
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
}
