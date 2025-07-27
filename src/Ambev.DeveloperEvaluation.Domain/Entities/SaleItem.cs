using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public SaleProduct Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public SaleItemStatus Status { get; set; }
    public decimal TotalPrice { get { return (Price * Quantity) - Discount; } }

    public class SaleProduct
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}