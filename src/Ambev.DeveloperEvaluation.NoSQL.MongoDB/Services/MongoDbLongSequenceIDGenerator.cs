using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Models;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Services;

public class MongoDbLongSequenceIDGenerator : ILongIDGenerator
{
    private readonly IMongoCollection<Counter> _collection;

    public MongoDbLongSequenceIDGenerator(IMongoDatabase database)
    {
        _collection = database.GetCollection<Counter>("counters");
    }

    public async Task<long> GenerateSaleNumber(CancellationToken cancellationToken)
    {
        return await Generate("sales", cancellationToken);
    }

    private async Task<long> Generate(string entityName, CancellationToken cancellationToken)
    {
        var filter = Builders<Counter>.Filter.Eq(c => c.Id, entityName);
        var update = Builders<Counter>.Update.Inc(c => c.Sequence, 1);

        var options = new FindOneAndUpdateOptions<Counter>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        var result = await _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

        return result.Sequence;
    }
}
