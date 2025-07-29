using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users._Shared;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class UserResultProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public UserResultProfile()
    {
        CreateMap<User, UserResult>();
    }
}
