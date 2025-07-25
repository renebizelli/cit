using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartCommand : CartKey, IRequest
{
}
