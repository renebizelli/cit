using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ListOptionsRequestValidator : AbstractValidator<ListOptionsRequest>
{
    public ListOptionsRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).WithMessage("##TODO: Page must be great than 0"); ;
        RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("##TODO: Page size must be great than 0"); ;
    }
}