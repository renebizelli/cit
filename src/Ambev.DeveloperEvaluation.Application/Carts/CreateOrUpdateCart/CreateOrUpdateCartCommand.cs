using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartCommand : CartKey, IRequest
{
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> Items { get; set; } = [];
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
