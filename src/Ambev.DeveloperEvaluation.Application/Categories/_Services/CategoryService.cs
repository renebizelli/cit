using Ambev.DeveloperEvaluation.Application.Categories._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Categories._Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly ICacheRepository _cache;

    public CategoryService(ICategoryRepository repository, ICacheRepository cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Category?> GetAsync(int categoryId, CancellationToken cancellationToken)
    {
        var category = await _cache.GetAsync<Category>(new CategoryCacheGetOptions(categoryId));
        if (category != null) return category;
            
        category = await _repository.GetAsync(categoryId, cancellationToken);
        
        if (category != null) await _cache.SetAsync(new CategoryCacheSetOptions(categoryId, TimeSpan.FromMinutes(30)), category);

        return category;
    }
}
