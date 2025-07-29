using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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

    public async Task<Sale> GetAsync(string id, long saleNumber, CancellationToken cancellationToken)
    {
        var filter = Builders<Sale>.Filter.Eq(s => s.Status, Domain.Enums.SaleStatus.Active);

        if (!string.IsNullOrEmpty(id)) filter &= Builders<Sale>.Filter.Eq(s => s.Id, id);

        if (saleNumber > 0) filter &= Builders<Sale>.Filter.Eq(s => s.SaleNumber, saleNumber);

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var filter = Builders<Sale>.Filter.Eq(s => s.Id, sale.Id);
        var updateOptions = new ReplaceOptions {};
        await _collection.ReplaceOneAsync(filter, sale, updateOptions, cancellationToken);
    }

}
