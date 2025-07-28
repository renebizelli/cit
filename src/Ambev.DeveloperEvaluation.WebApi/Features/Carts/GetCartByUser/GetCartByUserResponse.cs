namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserResponse
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public CartItemProduct Product { get; set; } = new();
        public int Quantity { get; set; }

        public class CartItemProduct
        {
            public string Id { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
        }
    }
}