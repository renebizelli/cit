namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserResult
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}