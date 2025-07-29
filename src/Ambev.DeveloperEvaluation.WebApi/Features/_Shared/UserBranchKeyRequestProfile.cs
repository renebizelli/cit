using Ambev.DeveloperEvaluation.Application._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features._Shared;

public class UserBranchKeyRequestProfile : Profile
{
    public UserBranchKeyRequestProfile()
    {
        CreateMap<UserBranchKeyRequest, UserBranchKey>()
            .ForMember(f => f.UserId, v => v.MapFrom(f => f.UserId))
            .ForMember(f => f.BranchId, v => v.MapFrom(f => f.BranchId));
    }
}
