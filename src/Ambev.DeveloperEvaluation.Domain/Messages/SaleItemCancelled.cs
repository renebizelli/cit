namespace Ambev.DeveloperEvaluation.Domain.Messages;

public class SaleItemCancelled
{
    public SaleItemCancelled(string saleId, string productId)
    {
        SaleId = saleId;
        ProdctId = productId;
    }

    public string SaleId { get; set; } = string.Empty;
    public string ProdctId { get; set; } = string.Empty;

}
