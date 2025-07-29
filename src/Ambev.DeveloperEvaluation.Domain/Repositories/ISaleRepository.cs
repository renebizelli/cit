using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);
    Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken);
    Task<long> CancelAsync(string saleId, CancellationToken cancellationToken);
    Task<(long, IList<Sale>)> ListSalesAsync(ISalesQuerySettings querySettings, Dictionary<string, Expression<Func<Sale, object>>> allowedOrderFields, CancellationToken cancellationToken);
}
