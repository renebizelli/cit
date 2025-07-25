using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string productId, CancellationToken cancellationToken);
    Task<Product?> GetAsync(string productId, CancellationToken cancellationToken);
    Task<bool> IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken);

    // Task<(int, IList<Product>)> ListProductsAsync(IProductQueryOptions queryOptions, CancellationToken cancellationToken);
}
