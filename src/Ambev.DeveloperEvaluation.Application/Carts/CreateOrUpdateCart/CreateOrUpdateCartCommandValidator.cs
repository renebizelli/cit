using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommandValidator : AbstractValidator<CreateOrUpdateCartCommand>
{
    public CreateOrUpdateCartCommandValidator()
    {
        Include(new CartKeyValidator());

        RuleFor(d => d.Items.Any()).NotEqual(false).WithMessage("##TODO: need to have at least one item");

        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new QuantitySaleItemValidator()).WithMessage("##TODO: quantity must to be great than zero");
                item.RuleFor(i => i.ProductId).NotEqual(0).WithMessage("##TODO: product id must to be great than zero");
            });
    }
}
