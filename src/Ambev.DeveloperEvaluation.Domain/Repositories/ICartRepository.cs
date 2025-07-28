using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserAsync(IUserBranchKey cartKey, CancellationToken cancellationToken = default);
    Task CreateOrUpdateCartAsync(Cart cart, CancellationToken cancellationToken = default);
    Task<long> DeleteCartAsync(IUserBranchKey cartKey, CancellationToken cancellationToken = default);
}
