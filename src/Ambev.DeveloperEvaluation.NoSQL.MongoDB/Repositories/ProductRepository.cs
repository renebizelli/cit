using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

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
                        .SetOnInsert(s => s.Id, product.Id);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);

        return product;
    }

    public async Task<bool> DeleteAsync(string productId, CancellationToken cancellationToken)
    {
        var filter =
            Builders<Product>.Filter.And(
            Builders<Product>.Filter.Eq(e => e.Id, productId),
            Builders<Product>.Filter.Eq(e => e.Active, true));

        var result = await _collection.DeleteOneAsync(filter, cancellationToken);

        return result.DeletedCount.Equals(1);
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
}
