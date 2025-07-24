
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface ICacheRepository
{
    Task<T?> GetAsync<T>(CacheGetOptions cacheGetOptions);
    Task SetAsync<T>(CacheSetOptions cacheSetOptions, T data);
}
