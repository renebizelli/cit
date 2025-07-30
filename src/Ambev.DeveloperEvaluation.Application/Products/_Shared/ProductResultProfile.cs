using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products._Shared;

public class ProductResultProfile : Profile
{
    public ProductResultProfile()
    {
        CreateMap<Product, ProductResult>();

        CreateMap<Product.RatingValues, ProductRatingResult>();

        CreateMap<Category, ProductCategoryResult>();
    }
}
