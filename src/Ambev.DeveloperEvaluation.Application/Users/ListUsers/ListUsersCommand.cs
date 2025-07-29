using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;
using Ambev.DeveloperEvaluation.Application.Users._Shared;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Userss.ListUsers;

public class ListUsersCommand : ListSettings, IRequest<PaginatedResult<UserResult>>, IUserQuerySettings
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime MinDate { get; set; } = DateTime.MinValue;
    public DateTime MaxDate { get; set; } = DateTime.MinValue;
}
