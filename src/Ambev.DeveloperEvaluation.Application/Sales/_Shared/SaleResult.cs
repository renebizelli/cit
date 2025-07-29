using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales._Shared;

public class SaleResult
{
    public string Id { get; set; } = string.Empty;
    public long SaleNumber { get; set; }
    public BranchResult Branch { get; set; } = new();
    public SaleStatus Status { get; set; }
    public UserResult User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SaleItemResult> Items { get; set; } = [];

    public class BranchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UserResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaleItemResult
    {
        public ProductResult Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public SaleItemStatus Status { get; set; }
        public decimal TotalPrice { get; set; }

        public class ProductResult
        {
            public string Id { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }
    }

}
