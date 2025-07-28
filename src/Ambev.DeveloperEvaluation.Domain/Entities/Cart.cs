using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : IUserBranchKey
{
    public string Id { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> Items { get; set; } = [];

    public Cart(IUserBranchKey userBranchKey)
    {
        UserId = userBranchKey.UserId;
        BranchId = userBranchKey.BranchId;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class CartItem
{
    public int Quantity { get; set; }
    public CartItemProduct Product { get; set; } = new();   

    public class CartItemProduct
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}