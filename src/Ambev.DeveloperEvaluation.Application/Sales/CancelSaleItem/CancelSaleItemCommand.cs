using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommand : IRequest
{
    public Guid UserId { get; set; } 
    public string SaleId { get; set; } = string.Empty;
    public string SaleItemId { get; set; } = string.Empty;
}
