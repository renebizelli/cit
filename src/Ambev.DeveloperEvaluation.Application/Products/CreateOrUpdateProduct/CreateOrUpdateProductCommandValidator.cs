using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductCommandValidator : AbstractValidator<CreateOrUpdateProductCommand>
{
    public CreateOrUpdateProductCommandValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .Length(10, 128)
            .WithMessage("Title is required and must be between 10 and 128 characters.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(product => product.CategoryId)
            .NotEqual(0)
            .WithMessage("CategoryId must be provided and greater than zero.");

        RuleFor(product => product.Price)
            .NotEqual(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .WithMessage("Image is required.");
    }
}
