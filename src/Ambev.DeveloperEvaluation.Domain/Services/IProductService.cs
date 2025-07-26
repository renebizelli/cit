using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IProductService
{
    Task<bool> IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken);
    Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken);
    Task EnrichWithCategoryAsync(int categoryId, Product product, CancellationToken cancellationToken);
}
