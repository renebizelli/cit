using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserAsync(CartKey cartKey, CancellationToken cancellationToken = default);
    Task CreateOrUpdateCartAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCartAsync(CartKey cartKey, CancellationToken cancellationToken = default);
}
