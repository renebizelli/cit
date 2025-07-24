using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CartKeyValidator : AbstractValidator<CartKey>
{
    public CartKeyValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("Invalid BranchId");
    }
}
