using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

public class ListSettingsRequestValidator : AbstractValidator<ListSettingsRequest>
{
    public ListSettingsRequestValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(request => request.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}