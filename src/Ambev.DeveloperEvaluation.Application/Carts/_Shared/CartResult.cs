namespace Ambev.DeveloperEvaluation.Application.Carts._Shared;

public class CartResult
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}