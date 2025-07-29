using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface IBranchRepository
{
    Task<Branch?> GetAsync(Guid id, CancellationToken cancellationToken);
}
