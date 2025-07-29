using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features._Shared;

public class UserBranchKeyRequestValidator : AbstractValidator<UserBranchKeyRequest>
{
    public UserBranchKeyRequestValidator()
    {
        RuleFor(f => f.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("UserId is required.");

        RuleFor(f => f.BranchId)
            .NotEqual(Guid.Empty)
            .WithMessage("BranchId is required.");
    }
}
