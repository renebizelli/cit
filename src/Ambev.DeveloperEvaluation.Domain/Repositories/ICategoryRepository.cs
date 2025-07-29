using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetAsync(int id, CancellationToken cancellationToken);
}
