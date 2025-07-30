using Ambev.DeveloperEvaluation.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Mapping;

public class ProductMapping
{
    public ProductMapping()
    {
        BsonClassMap.RegisterClassMap<Product>(map =>
        {
            map.MapIdProperty(p => p.Id);
            map.MapProperty(p => p.Title).SetElementName("title");
            map.MapProperty(p => p.Description).SetElementName("desc");
            map.MapProperty(p => p.Price).SetElementName("price");
            map.MapProperty(p => p.Image).SetElementName("img");
            map.MapProperty(p => p.Active).SetElementName("active");
            map.MapProperty(p => p.Category).SetElementName("category");
            map.MapProperty(p => p.Rating).SetElementName("rating");
        });

        BsonClassMap.RegisterClassMap<Category>(map =>
        {
            map.MapProperty(p => p.Id).SetElementName("id");
            map.MapProperty(p => p.Name).SetElementName("name");
        });

        BsonClassMap.RegisterClassMap<Product.RatingValues>(map =>
        {
            map.MapProperty(p => p.Count).SetElementName("count");
            map.MapProperty(p => p.Avg).SetElementName("avg");
        });
    }
}
