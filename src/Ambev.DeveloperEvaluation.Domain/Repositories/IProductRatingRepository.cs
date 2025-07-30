using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRatingRepository
{
    Task SaveAsync(ProductRating productRating, CancellationToken cancellationToken);
    Task<(long, double)> GetAverageRatingAsync(string productId, CancellationToken cancellationToken);

}
