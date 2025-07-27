using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Products._Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryService _categoryService;

    public ProductService(
        IProductRepository repository,
        ICategoryService categoryService)
    {
        _repository = repository;
        _categoryService = categoryService;
    }

    public async Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var isNameAlreadyUsed = await _repository.IsNameAlreadyUsedAsync(product.Title.Trim(), cancellationToken);
        if (isNameAlreadyUsed) throw new InvalidOperationException("##TODO isNameAlreadyUsed");

        await EnrichWithCategoryAsync(product, cancellationToken);

        product.Activate();

        return await _repository.CreateOrUpdateAsync(product, cancellationToken);
    }

    private async Task EnrichWithCategoryAsync(Product product, CancellationToken cancellationToken)
    {
        if (product.Category == null ||
            product.Category.Id.Equals(0)) throw new InvalidOperationException("##TODO Invalid Category");

        var category = await _categoryService.GetAsync(product.Category.Id, cancellationToken);

        if (category != null)
        {
            product.Category = category;
        }
    }

    public async Task<(long, IList<Product>)> ListProductsAsync(IProductQuerySettings querySettings, CancellationToken cancellationToken)
    {
        var allowedOrderFields = new Dictionary<string, Expression<Func<Product, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["title"] = p => p.Title,
            ["price"] = p => p.Price,
            ["category"] = p => p.Category!.Name,
        };

        return await _repository.ListProductsAsync(
            querySettings,
            allowedOrderFields,
            cancellationToken);
    }
}
