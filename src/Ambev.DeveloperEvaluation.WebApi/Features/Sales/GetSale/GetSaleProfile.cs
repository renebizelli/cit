using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleRequest, GetSaleCommand>()
            .AfterMap((src, desc) =>
            {
                if(long.TryParse(src.Value, out var saleNumber))
                {
                    desc.SaleNumber = saleNumber;
                    return;
                }

                desc.SaleId = src.Value;
            });
    }
}
