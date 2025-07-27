using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IProductService
{
    Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken);
    Task<(long, IList<Product>)> ListProductsAsync(IProductQuerySettings querySettings, CancellationToken cancellationToken);

}
