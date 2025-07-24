using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartRequestValidator : AbstractValidator<CreateOrUpdateCartRequest>
{
    public CreateOrUpdateCartRequestValidator()
    {
        Include(new CartBaseRequestValidator());

        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new QuantitySaleItemValidator()).WithMessage("quantity must to be great than zero");
            });
    }
}
