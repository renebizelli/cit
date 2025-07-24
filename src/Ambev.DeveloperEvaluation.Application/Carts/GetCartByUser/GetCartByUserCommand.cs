using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
