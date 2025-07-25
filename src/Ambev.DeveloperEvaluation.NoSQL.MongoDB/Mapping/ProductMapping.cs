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
            map.MapProperty(p => p.Category).SetElementName("category");
        });

        BsonClassMap.RegisterClassMap<ProductCategory>(map =>
        {
            map.MapIdProperty(p => p.Id);
            map.MapProperty(p => p.Name).SetElementName("name");
        });
    }
}
