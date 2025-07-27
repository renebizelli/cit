using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;


public class CreateOrUpdateProductCommandValidator : AbstractValidator<CreateOrUpdateProductCommand>
{
    public CreateOrUpdateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(10, 128).WithMessage("##TODO");
        RuleFor(product => product.Description).NotEmpty().WithMessage("##TODO");
        RuleFor(product => product.CategoryId).NotEqual(0).WithMessage("##TODO");
        RuleFor(product => product.Price).NotEqual(0).WithMessage("##TODO");
        RuleFor(product => product.Image).NotEmpty().WithMessage("##TODO");
    }
}
