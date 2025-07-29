using Ambev.DeveloperEvaluation.Application._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        Include(new UserBranchKeyValidator());
    }
}
