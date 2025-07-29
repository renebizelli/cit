using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    public ListUsersRequestValidator()
    {
        RuleFor(x => x.Username)
            .MinimumLength(3)
            .When(x => !string.IsNullOrEmpty(x.Username))
            .WithMessage("##TODO: The username must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .MinimumLength(3)
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("##TODO: The email must not exceed 100 characters.");

        RuleFor(x => x)
            .Must(x =>
                x.MinDate.Equals(DateTime.MinValue) ||
                x.MaxDate.Equals(DateTime.MinValue) ||
                x.MinDate <= x.MaxDate)
            .WithMessage("##TODO: The min date must be less than or equal to the max date.");

        RuleFor(x => x.Role).IsInEnum<ListUsersRequest, UserRole>()
            .WithMessage("##TODO: Invalid role");

        RuleFor(x => x.Status).IsInEnum<ListUsersRequest, UserStatus>()
            .WithMessage("##TODO: Invalid status");

    }
}
