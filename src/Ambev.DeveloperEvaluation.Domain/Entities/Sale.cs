using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public string Id { get; set; } = string.Empty;
    public SaleBranch Branch { get; set; } = new();
    public SaleStatus Status { get; set; }
    public SaleUser User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SaleItem> Items { get; set; } = [];

    public class SaleItem
    {
        public SaleProduct Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public SaleItemStatus Status { get; set; }
        public decimal TotalPrice { get { return (Price * Quantity) - Discount; } }
    }

    public class SaleProduct
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class SaleBranch
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaleUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
