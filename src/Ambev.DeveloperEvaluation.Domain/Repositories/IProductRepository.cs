﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken);
    Task<long> DeleteAsync(string productId, CancellationToken cancellationToken);
    Task<Product?> GetAsync(string productId, CancellationToken cancellationToken);
    Task<bool> IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken);
    Task<(long, IList<Product>)> ListProductsAsync(IProductQuerySettings querySettings, Dictionary<string, Expression<Func<Product, object>>> allowedOrderFields, CancellationToken cancellationToken);
    Task<IList<Product>> ListAllActiveAsync(CancellationToken cancellationToken);
    Task RatingUpdateAsync(string productId, Product.RatingValues rating, CancellationToken cancellationToken);
}
