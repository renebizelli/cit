using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Policies;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sale  
{
    public Sale(string id, long saleNumber, SaleBranch branch, SaleUser user)
    {
        Id = id;
        SaleNumber = saleNumber;
        Branch = branch;
        User = user;
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Active;
    }

    public string Id { get; private set; } = string.Empty;
    public long SaleNumber { get; private set; }
    public SaleBranch Branch { get; private set; }
    public SaleStatus Status { get; private set; }
    public SaleUser User { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public decimal TotalAmount { get; private set; }
    public List<SaleItem> Items { get; private set; } = new List<SaleItem>();

    public ICollection<SaleItem> ActiveItems() => Items.Where(w => w.Status == SaleItemStatus.Active).ToList();
    private void CalculateTotalAmount() => TotalAmount = ActiveItems().Sum(item => item.TotalPrice);
    public void AddItem(SaleItem item)
    {
        Items.Add(item);
        AttachEvents(item);
        CalculateTotalAmount();
    }

    private void AttachEvents(SaleItem item)
    {
        item.OnChanged += CalculateTotalAmount;
        item.OnCancel += AutoCancelIfAllItemsCanceled;
    }

    public void AttachEvents()
    {
        foreach (var item in ActiveItems())
        {
            AttachEvents(item);
        }
    }

    public void Cancel()
    {
        Status = SaleStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;
        CalculateTotalAmount();
    }

    public void AutoCancelIfAllItemsCanceled()
    {
        if(ActiveItems().Count.Equals(0))
        {
            Cancel();
        }
    }

    public void ResetDiscounts()
    {
        foreach (var item in Items)
        {
            item.ResetDiscount();
        }
    }

    public class SaleItem
    {
        public SaleItem(string id, SaleProduct product, int quantity)
        {
            Id = id;
            Product = product;
            Quantity = quantity;

            Status = SaleItemStatus.Active;

            CalculateTotalPrice();
        }

        public string Id { get; private set; }
        public SaleProduct Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Discount { get; private set; }
        public SaleItemStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }

        public event Action? OnChanged;
        public event Action? OnCancel;

        public void CalculateTotalPrice() { TotalPrice = (Product.Price * Quantity) - Discount; }

        public void ApplyDiscount(IDiscountPolicy policy)
        {
            Discount = policy.Apply(this);

            CalculateTotalPrice();

            OnChanged?.Invoke();
        }

        public void ResetDiscount()
        {
            Discount = 0;
            CalculateTotalPrice();
            OnChanged?.Invoke();
        }

        public void Cancel()
        {
            if (Status == SaleItemStatus.Canceled) return;
            Status = SaleItemStatus.Canceled;
            OnChanged?.Invoke();
            OnCancel?.Invoke();
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
        public string Id { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

    }

    public class SaleBranch
    {
        public SaleBranch(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
    }

    public class SaleUser
    {
        public SaleUser(Guid id, string name, string? city, string? state)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? City { get; private set; } = string.Empty;
        public string? State { get; private set; } = string.Empty;
    }
}
