using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartRequest : CartBaseRequest
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
