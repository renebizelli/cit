using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommand : CartKey,  IRequest<GetCartByUserResult>
{
}
