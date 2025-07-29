using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Users._Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<(long, IList<User>)> ListUsersAsync(IUserQuerySettings querySettings, CancellationToken cancellationToken)
    {
        var allowedOrderFields = new Dictionary<string, Expression<Func<User, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["username"] = p => p.Username,
            ["email"] = p => p.Email,
            ["role"] = p => p.Role,
            ["createdAt"] = p => p.CreatedAt
        };

        return await _userRepository.ListUsersAsync(
            querySettings,
            allowedOrderFields,
            cancellationToken);
    }
}
