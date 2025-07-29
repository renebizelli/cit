namespace Ambev.DeveloperEvaluation.Domain.Messages;

public class SaleCancelled
{
    public string SaleId { get; set; } = string.Empty;

    public SaleCancelled(string saleId)
    {
        SaleId = saleId;
    }
}
