using Ambev.DeveloperEvaluation.WebApi.Features._Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartRequest : UserBranchKeyRequest
{
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
