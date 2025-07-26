using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

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

    public async Task<bool> IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken)
    => await _repository.IsNameAlreadyUsedAsync(title.Trim(), cancellationToken);

    public async Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken)
    {
        return await _repository.CreateOrUpdateAsync(product, cancellationToken);
    }

    public async Task EnrichWithCategoryAsync(int categoryId, Product product, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetAsync(categoryId, cancellationToken);

        if (category != null)
        {
            product.Category = category;
        }
    }
}
