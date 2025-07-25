using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommandValidator : AbstractValidator<CreateOrUpdateCartCommand>
{
    public CreateOrUpdateCartCommandValidator()
    {
        Include(new CartKeyValidator());

        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new QuantitySaleItemValidator()).WithMessage("quantity must to be great than zero");
            });
    }
}
