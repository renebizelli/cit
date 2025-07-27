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

    public class SaleBranch
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaleUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
