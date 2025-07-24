namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserResponse
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}