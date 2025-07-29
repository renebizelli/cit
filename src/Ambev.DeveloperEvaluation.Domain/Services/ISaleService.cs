using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<Sale> CreateAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken);
    Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken = default);
}
