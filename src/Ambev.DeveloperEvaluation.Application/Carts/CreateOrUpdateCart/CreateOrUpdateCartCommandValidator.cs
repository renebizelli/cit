﻿using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommandValidator : AbstractValidator<CreateOrUpdateCartCommand>
{
    public CreateOrUpdateCartCommandValidator()
    {
        Include(new UserBranchKeyValidator());

        RuleFor(d => d.Items.Any()).NotEqual(false).WithMessage("need to have at least one item");

        RuleForEach(d => d.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.Quantity).SetValidator(new QuantitySaleItemValidator()).WithMessage("quantity must to be great than zero");
                item.RuleFor(i => i.ProductId).NotEmpty().WithMessage("product id must to be great than zero");
            });
    }
}
