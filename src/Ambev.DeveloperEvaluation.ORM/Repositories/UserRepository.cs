﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<(int, IList<User>)> ListUsersAsync(IUserQuerySettings querySettings, Dictionary<string, Expression<Func<User, object>>> allowedOrderFields, CancellationToken cancellationToken)
    {
        var query = _context.Users
                    .Include(a => a.Address)
                    .AsNoTracking()
                    .ApplyWhereLike(querySettings.Username, w => w.Username)
                    .ApplyWhereLike(querySettings.Email, w => w.Email)
                    .ApplyWhereRange(querySettings.MinDate, querySettings.MinDate, w => w.CreatedAt)
                    .ApplyWhereIfNotNone(querySettings.Status, w => w.Status)
                    .ApplyWhereIfNotNone(querySettings.Role, w => w.Role)
                    .ApplyOrdering(querySettings.OrderSettings, allowedOrderFields);

        var count = await query.CountAsync();

        var products = await query
                        .ApplyPaging(querySettings.PagingSettings)
                        .ToListAsync(cancellationToken);

        return (count, products);
    }
}
