using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    public CancelSaleItemRequestValidator()
    {
        RuleFor(f => f.SaleId).NotEmpty().WithMessage("##TODO: SaleId must be great than zero");
        RuleFor(f => f.SaleItemId).NotEmpty().WithMessage("##TODO: SaleItemId must be great than zero");
    }
}
