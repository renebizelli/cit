using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;

public class CartBaseRequestValidator : AbstractValidator<CartBaseRequest>
{
    public CartBaseRequestValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("Invalid BranchId");
    }
}
