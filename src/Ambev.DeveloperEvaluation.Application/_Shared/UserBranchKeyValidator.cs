using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application._Shared;

public class UserBranchKeyValidator : AbstractValidator<UserBranchKey>
{
    public UserBranchKeyValidator()
    {
        RuleFor(f => f.UserId).NotEqual(Guid.Empty).WithMessage("##TODO: Invalid UserId");
        RuleFor(f => f.BranchId).NotEqual(Guid.Empty).WithMessage("##TODO: Invalid BranchId");
    }
}
