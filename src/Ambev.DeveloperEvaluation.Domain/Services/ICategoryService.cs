using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ICategoryService
{
    Task<Category?> GetAsync(int categoryId, CancellationToken cancellationToken);
}

