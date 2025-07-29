namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleRequest
{
    public Guid UserId { get; set; }
    public string Id { get; set; } = string.Empty;
}
