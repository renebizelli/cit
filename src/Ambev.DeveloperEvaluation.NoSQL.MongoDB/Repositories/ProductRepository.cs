using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Product>("products");
    }

    public async Task<Product> CreateOrUpdateAsync(Product product, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(product.Id)) product.Id = ObjectId.GenerateNewId().ToString();

        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Id, product.Id),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        var update = Builders<Product>.Update
                        .Set(s => s.Title, product.Title)
                        .Set(s => s.Description, product.Description)
                        .Set(s => s.Price, product.Price)
                        .Set(s => s.Image, product.Image)
                        .Set(s => s.Category, product.Category)
                        .SetOnInsert(s => s.Active, true)
                        .SetOnInsert(s => s.Id, product.Id)
                        .SetOnInsert(s => s.Rating, product.Rating);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

        return product;
    }

    public async Task RatingUpdateAsync(string productId,  Product.RatingValues rating, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId)) throw new ArgumentNullException(nameof(productId));
        if (rating == null) throw new ArgumentNullException(nameof(productId));

        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Id, productId),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        var update = Builders<Product>.Update
                        .Set(s => s.Rating, rating);

        await _collection.UpdateOneAsync(filter, update, new(), cancellationToken);
    }

    public async Task<long> DeleteAsync(string productId, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Id, productId),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        var update = Builders<Product>.Update
                        .Set(s => s.Active, false);

        var result = await _collection.UpdateOneAsync(filter, update, new UpdateOptions(), cancellationToken);

        return result.ModifiedCount;
    }

    public async Task<Product?> GetAsync(string productId, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Id, productId),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        var result = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<bool> IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Title, title),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        return await _collection.Find(filter).AnyAsync(cancellationToken);
    }

    public async Task<(long, IList<Product>)> ListProductsAsync(IProductQuerySettings querySettings, Dictionary<string, Expression<Func<Product, object>>> allowedOrderFields, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(e => e.Active, true);

        filter = filter.ApplyWhereLike(querySettings.Title, e => e.Title);
        filter = filter.ApplyWhereLike(querySettings.Description, e => e.Description);
        filter = filter.ApplyWhereRange(querySettings.MinPrice, querySettings.MaxPrice, e => e.Price);

        var query = _collection.Find(filter);

        var count = await query.CountDocumentsAsync(cancellationToken);

        var products = await query
                         .ApplyOrdering(querySettings.OrderSettings, allowedOrderFields)
                         .ApplyPaging(querySettings.PagingSettings)
                         .ToListAsync(cancellationToken);

        return (count, products);
    }

    public async Task<IList<Product>> ListAllActiveAsync(CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Eq(e => e.Active, true);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }   
}

