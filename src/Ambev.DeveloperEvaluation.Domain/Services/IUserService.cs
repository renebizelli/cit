using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IUserService
{
    Task<(long, IList<User>)> ListUsersAsync(IUserQuerySettings querySettings, CancellationToken cancellationToken);
}