using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<Sale> CreateAsync(IUserBranchKey userBranchKey, CancellationToken cancellationToken);
    Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken);
    Task CancelSaleItemAsync(string saleId, string saleItemId, CancellationToken cancellationToken);
    Task CancelAsync(string saleId, CancellationToken cancellationToken);
    Task<(long, IList<Sale>)> ListSalesAsync(ISalesQuerySettings querySettings, CancellationToken cancellationToken);

}
