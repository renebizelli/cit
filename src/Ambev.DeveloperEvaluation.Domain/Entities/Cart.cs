using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : IUserBranchKey
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> Items { get; set; } = [];

    public Cart() {}
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
    public string ProductId { get; set; } = string.Empty;
}