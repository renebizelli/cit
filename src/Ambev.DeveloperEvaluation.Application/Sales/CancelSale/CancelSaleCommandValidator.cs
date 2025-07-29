﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    public CancelSaleCommandValidator()
    {
        RuleFor(f => f.Id).NotEmpty().WithMessage("##TODO: SaleId must not be empty");
    }
}
