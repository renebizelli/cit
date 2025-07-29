using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    public CancelSaleItemCommandValidator()
    {
        RuleFor(f => f.SaleId).NotEmpty().WithMessage("SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEmpty().WithMessage("SaleItemId must be great than zero");
    }
}
