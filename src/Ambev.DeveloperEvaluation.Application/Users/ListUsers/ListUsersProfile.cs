using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Users._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Userss.ListUsers;

internal class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        CreateMap<(long TotalCount, IList<User> Users), PaginatedResult<UserResult>>()
            .ForMember(f => f.TotalCount, o => o.MapFrom(m => m.TotalCount))
            .ForMember(f => f.Items, o => o.MapFrom(m => m.Users));

        CreateMap<User, UserResult>();
    }
}



 