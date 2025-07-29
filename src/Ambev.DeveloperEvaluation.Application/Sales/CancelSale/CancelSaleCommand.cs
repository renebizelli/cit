using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleCommand : IRequest
{
    public Guid UserId { get; set; } 
    public string Id { get; set; } = string.Empty;
}
