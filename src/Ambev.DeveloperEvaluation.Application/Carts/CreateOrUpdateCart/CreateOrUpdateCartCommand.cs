using Ambev.DeveloperEvaluation.Application._Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommand : UserBranchKey, IRequest
{
    public DateTime UpdatedAt { get; set; }
    public ICollection<CartItem> Items { get; set; } = [];

    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
