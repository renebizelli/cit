using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Policies;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Sale(string id, SaleBranch branch, SaleUser? user)
    {
        Id = id;
        Branch = branch;
        User = user;
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Active;
    }

    public string Id { get; set; } = string.Empty;
    public SaleBranch Branch { get; set; }
    public SaleStatus Status { get; set; }
    public SaleUser? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get { return CalculateTotalAmount(); } }
    public ICollection<SaleItem> Items { get; set; } = [];
    public ICollection<SaleItem> ActiveItems() => Items.Where(w => w.Status == SaleItemStatus.Active).ToList();

    private decimal CalculateTotalAmount() => Items.Where(w => w.Status == SaleItemStatus.Active).Sum(item => item.TotalPrice);

    public class SaleItem
    {
        public SaleItem(string id, SaleProduct product, int quantity)
        {
            Id = id;
            Product = product;
            Quantity = quantity;

            Status = SaleItemStatus.Active;
        }

        public string Id { get; set; }
        public SaleProduct Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public SaleItemStatus Status { get; set; }
        public decimal TotalPrice { get { return CalculateTotalPrice(); } }

        private decimal CalculateTotalPrice() => (Product.Price * Quantity) - Discount;

        public void ApplyDiscount(IDiscountPolicy policy)
        {
            Discount = policy.Apply(this);
        }
    }

    public class SaleProduct
    {
        public SaleProduct(string id, string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
        }

        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }

    public class SaleBranch
    {
        public SaleBranch(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaleUser
    {
        public SaleUser(Guid id, string name, string city, string state)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
