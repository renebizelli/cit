using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Models;
using MongoDB.Bson.Serialization;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Mapping;

public class CounterMapping
{
    public CounterMapping()
    {
        BsonClassMap.RegisterClassMap<Counter>(map =>
        {
            map.MapIdProperty(p => p.Id);
            map.MapProperty(p => p.Sequence).SetElementName("sequence");
        });
    }
}
