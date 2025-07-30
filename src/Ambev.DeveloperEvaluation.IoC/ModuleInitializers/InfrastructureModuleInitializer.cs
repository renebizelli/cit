using Ambev.DeveloperEvaluation.Cache.Redis;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Messaging.Rebus;
using Ambev.DeveloperEvaluation.NoSQL.MongoDB;
using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Repositories;
using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Services;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ICacheRepository, RedisRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<IBranchRepository, BranchRepository>();
        builder.Services.AddScoped<IStringIDGenerator, MongoDbStringIDGenerator>();
        builder.Services.AddScoped<ILongIDGenerator, MongoDbLongSequenceIDGenerator>();
        builder.Services.AddScoped<IProductRatingRepository, ProductRatingRepository>();

        

        new MongoDbInitialize().Initialize(builder);

        builder.AddRedis();
        builder.Services.AddMessageHandlers();
    }
}