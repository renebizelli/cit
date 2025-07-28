using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleResult, SaleDetailResponse>()
                .IncludeBase<SaleResult, SaleResponse>();

            CreateMap<SaleResult, SaleResponse>()
                .ForMember(f => f.Branch, o => o.MapFrom(m => m.Branch));

            CreateMap<SaleResult.SaleItemResult, SaleResponse.SaleItemResponse>();
            CreateMap<SaleResult.UserResult, SaleResponse.UserResponse>();
            CreateMap<SaleResult.SaleItemResult.ProductResult, SaleResponse.SaleItemResponse.ProductResponse>();
            CreateMap<SaleResult.BranchResult, SaleResponse.BranchResponse>();
        }
    }
}
