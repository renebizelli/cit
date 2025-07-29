using Ambev.DeveloperEvaluation.WebApi.Features._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        Include(new UserBranchKeyRequestValidator());
    }
}
