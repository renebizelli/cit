using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;
using Ambev.DeveloperEvaluation.Application.Users._Shared;
using Ambev.DeveloperEvaluation.Application.Userss.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;
using Ambev.DeveloperEvaluation.WebApi.Features.Users._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        CreateMap<ListUsersRequest, ListUsersCommand>()
            .IncludeBase<ListSettingsRequest, ListSettings>();

        CreateMap<UserResult, UserResponse>();
        CreateMap<PaginatedResult<UserResult>, Paginated<UserResponse>>();
    }
}
