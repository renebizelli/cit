using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    public CancelSaleItemCommandValidator()
    {
        RuleFor(f => f.SaleId).NotEmpty().WithMessage("##TODO: SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEmpty().WithMessage("##TODO: SaleItemId must be great than zero");
    }
}
