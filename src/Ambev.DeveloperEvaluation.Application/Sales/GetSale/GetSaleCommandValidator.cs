using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleCommandValidator()
    {
        RuleFor(f => f)
            .Must(f => !string.IsNullOrEmpty(f.SaleId) || !f.SaleNumber.Equals(0))
            .WithMessage("##TODO: Id or Number need to be provided");
    }
}
