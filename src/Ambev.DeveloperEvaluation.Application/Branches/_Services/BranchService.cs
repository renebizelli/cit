using Ambev.DeveloperEvaluation.Application.Branches._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Branches._Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _repository;
    private readonly ICacheRepository _cache;

    public BranchService(
        IBranchRepository repository,
        ICacheRepository cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Branch?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var branch = await _cache.GetAsync<Branch>(new BranchCacheGetOptions(id));
        if (branch != null) return branch;

        branch = await _repository.GetAsync(id, cancellationToken);

        if (branch != null) await _cache.SetAsync(new BranchCacheSetOptions(id, TimeSpan.FromMinutes(30)), branch);

        return branch;
    }
}
