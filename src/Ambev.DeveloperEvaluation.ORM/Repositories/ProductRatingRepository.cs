using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRatingRepository : IProductRatingRepository
{
    private readonly DefaultContext _context;
    public ProductRatingRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(ProductRating productRating, CancellationToken cancellationToken)
    {
        var rating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.ProductId == productRating.ProductId && x.UserId == productRating.UserId, cancellationToken);

        if (rating == null)
        {
            _context.ProductRatings.Add(productRating);
        }
        else
        {
            rating.CreatedAt = productRating.CreatedAt;
            rating.Rating = productRating.Rating;
            _context.ProductRatings.Update(rating);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(long, double)> GetAverageRatingAsync(string productId, CancellationToken cancellationToken)
    {
        var count = await _context.ProductRatings
            .Where(x => x.ProductId == productId)
            .CountAsync(cancellationToken);

        if (count > 0)
        {
            var rating = await _context.ProductRatings
                .Where(x => x.ProductId == productId)
                .AverageAsync(x => x.Rating, cancellationToken);

            return (count, rating);
        }

        return (count, 0);
    }
}
