namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

public class CancelSaleItemRequest
{
    public Guid UserId { get; set; }
    public string SaleId { get; set; } = string.Empty;
    public string SaleItemId { get; set; } = string.Empty;
}
