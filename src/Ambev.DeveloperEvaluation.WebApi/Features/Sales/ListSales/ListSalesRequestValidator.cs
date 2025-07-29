using Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;


public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    public ListSalesRequestValidator()
    {
        Include(new ListSettingsRequestValidator());

        RuleFor(request => request)
            .Must(m =>
                ((m.MinTotalAmount > 0 && m.MaxTotalAmount > 0) && m.MinTotalAmount <= m.MaxTotalAmount) ||
                (m.MinTotalAmount == 0 || m.MaxTotalAmount == 0))
                .WithMessage("##TODO: must be zero or minTotalPrice <= maxTotalPrice");

        RuleFor(x => x)
            .Must(x =>
                x.MinDate.Equals(DateTime.MinValue) ||
                x.MaxDate.Equals(DateTime.MinValue) ||
                x.MinDate <= x.MaxDate)
            .WithMessage("##TODO: The min date must be less than or equal to the max date.");
    }
}
