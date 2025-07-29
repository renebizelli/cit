using Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesRequest : ListSettingsRequest
{
    public Guid UserId { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public DateTime MinDate { get; set; } = DateTime.MinValue;
    public DateTime MaxDate { get; set; } = DateTime.MinValue;
    public decimal MinTotalAmount { get; set; }
    public decimal MaxTotalAmount { get; set; }
}
