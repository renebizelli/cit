using Ambev.DeveloperEvaluation.Application._Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommand : UserBranchKey,  IRequest<GetCartByUserResult>
{
}
