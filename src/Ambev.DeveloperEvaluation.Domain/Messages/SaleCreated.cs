using Ambev.DeveloperEvaluation.Domain.Entities;
namespace Ambev.DeveloperEvaluation.Domain.Messages;

public class SaleCreated
{
    public Sale Sale { get; set; }

    public SaleCreated(Sale sale)
    {
        Sale = sale;
    }
}
