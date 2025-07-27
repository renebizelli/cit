namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface ISalesQueryOptions : IListSettings
{
    Guid UserId { get; set; }
    Guid BranchId { get; set; }
    DateTime MinDate { get; set; } 
    DateTime MaxDate { get; set; }
    decimal MinTotalPrice { get; set; }
    decimal MaxTotalPrice { get; set; }
}
