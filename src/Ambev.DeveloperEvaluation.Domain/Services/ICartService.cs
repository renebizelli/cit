using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ICartService
{
    Task CreateOrUpdateAsync(Cart cart, CancellationToken cancellationToken = default);
    Task DeleteAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default);
    Task<Cart> GetByUserAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default);
    Task<Cart?> GetByUserForSaleCreatingAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken = default);
}
