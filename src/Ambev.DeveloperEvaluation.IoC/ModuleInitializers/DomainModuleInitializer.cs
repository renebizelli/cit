using Ambev.DeveloperEvaluation.Application.Branches._Services;
using Ambev.DeveloperEvaluation.Application.Carts._Services;
using Ambev.DeveloperEvaluation.Application.Categories._Services;
using Ambev.DeveloperEvaluation.Application.Products._Services;
using Ambev.DeveloperEvaluation.Application.Sales._Services;
using Ambev.DeveloperEvaluation.Application.Users._Services;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAllowedDiscountSpecification, AllowedDiscountSpecification>();
        builder.Services.AddScoped<IActiveUserSpecification, ActiveUserSpecification>();
        builder.Services.AddScoped<IMinimumActiveSaleItemSpecification, MinimumActiveSaleItemSpecification>();
        builder.Services.AddScoped<IMinimumCartItemSpecification, MinimumCartItemSpecification>();
        builder.Services.AddScoped<ISaleItemQuantityWithinRangeSpecification, SaleItemQuantityWithinRangeSpecification>();
        builder.Services.AddScoped<IDiscountFactory, DiscountFactory>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ISaleService, SaleService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<ISalePricingService, SalePricingService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBranchService, BranchService>();
    }
}
