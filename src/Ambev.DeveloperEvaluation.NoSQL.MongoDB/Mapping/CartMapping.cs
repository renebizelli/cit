using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Mapping;

public class CartMapping
{
    public CartMapping()
    {
        BsonClassMap.RegisterClassMap<Cart>(map =>
        {
            map.MapIdProperty(p => p.Id);
            map.MapIdProperty(p => p.Id);
            map.MapProperty(p => p.UserId).SetElementName("user");
            map.MapProperty(p => p.UpdatedAt).SetElementName("update");
            map.MapProperty(p => p.Items).SetElementName("items");
            map.MapProperty(p => p.BranchId).SetElementName("branch");
        });

        BsonClassMap.RegisterClassMap<CartItem>(map =>
        {
            map.MapProperty(p => p.ProductId).SetElementName("product");
            map.MapProperty(p => p.Quantity).SetElementName("quantity");
        });
    }
}
