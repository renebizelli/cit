namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart
{
    public string Id { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> Items { get; set; } = [];
}

public class CartItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}