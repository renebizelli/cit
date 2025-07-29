using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<Sale> CreateAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken);
    Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken = default);
    Task CancelSaleItemAsync(string saleId, string saleItemId, CancellationToken cancellationToken = default);
    Task CancelAsync(string saleId, CancellationToken cancellationToken = default);

}
