using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    public CancelSaleItemRequestValidator()
    {
        RuleFor(f => f.SaleId).NotEmpty().WithMessage("SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEmpty().WithMessage("SaleItemId must be great than zero");
    }
}
