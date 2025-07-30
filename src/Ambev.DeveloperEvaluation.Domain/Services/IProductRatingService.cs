using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IProductRatingService
{
    Task SaveAsync(ProductRating productRating, CancellationToken cancellationToken);
    Task RecalculateAsync(CancellationToken cancellationToken);
}
