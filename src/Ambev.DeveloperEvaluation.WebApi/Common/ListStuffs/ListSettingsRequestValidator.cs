using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

public class ListSettingsRequestValidator : AbstractValidator<ListSettingsRequest>
{
    public ListSettingsRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).WithMessage("##TODO: Page must be great than 0"); ;
        RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("##TODO: Page size must be great than 0"); ;
    }
}