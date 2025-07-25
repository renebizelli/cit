using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(10, 128).WithMessage("##TODO");
        RuleFor(product => product.Description).NotEmpty().WithMessage("##TODO");
        RuleFor(product => product.CategoryId).NotEqual(0).WithMessage("##TODO");
        RuleFor(product => product.Price).NotEqual(0).WithMessage("##TODO");
        RuleFor(product => product.Image).NotEmpty().WithMessage("##TODO");
    }
}
