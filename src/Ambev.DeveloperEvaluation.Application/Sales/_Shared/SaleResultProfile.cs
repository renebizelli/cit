using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales._Shared;

public class SaleResultProfile : Profile
{
    public SaleResultProfile()
    {
        CreateMap<Sale, SaleResult>();
        CreateMap<Sale.SaleItem, SaleResult.SaleItemResult>();
        CreateMap<Sale.SaleProduct, SaleResult.SaleItemResult.ProductResult>();
        CreateMap<Sale.SaleBranch, SaleResult.BranchResult>();
        CreateMap<Sale.SaleUser, SaleResult.UserResult>();
    }
}
