namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;

public class CartResponse
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}