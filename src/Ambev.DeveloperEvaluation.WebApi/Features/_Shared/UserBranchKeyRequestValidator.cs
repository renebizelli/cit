using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features._Shared;

public class UserBranchKeyRequestValidator : AbstractValidator<UserBranchKeyRequest>
{
    public UserBranchKeyRequestValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("##TODO: Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("##TODO: Invalid BranchId");
    }
}
