using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.ProductRatings._Services;

public class ProductRatingService : IProductRatingService
{
    private readonly IProductRatingRepository _repository;
    private readonly IProductService _productService;

    public ProductRatingService(IProductRatingRepository repository, IProductService productService)
    {
        _repository = repository;
        _productService = productService;
    }

    public async Task SaveAsync(ProductRating productRating, CancellationToken cancellationToken)
    {
        await _repository.SaveAsync(productRating, cancellationToken);
    }

    public async Task RecalculateAsync(CancellationToken cancellationToken)
    {
        var products = await _productService.ListAllActiveAsync(cancellationToken);

        foreach (var product in products)
        {
            var (count, avg) = await _repository.GetAverageRatingAsync(product.Id, cancellationToken);

            await _productService.RatingUpdateAsync(product.Id, new Product.RatingValues { Count = count, Avg = avg }, cancellationToken);
        }
    }
}
