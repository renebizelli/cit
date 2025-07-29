using Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

public class ListSettingsProfile : Profile
{
    public ListSettingsProfile()
    {
        CreateMap<ListSettingsRequest, ListSettings>()
        .AfterMap((src, dest) =>
        {
            dest.OrderSettings = new OrderSettings()
            {
                Criteria = OrderCriteriaBuilder.Build(src._Order, "title")
            };

            dest.PagingSettings = new PagingSettings()
            {
                Page = src.Page,
                Size = src.PageSize
            };

        });
    }
}
