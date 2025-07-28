using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly IMongoCollection<Sale> _collection;

    public SaleRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Sale>("sales");
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(sale, new InsertOneOptions(), cancellationToken);

        return sale;
    }
}
