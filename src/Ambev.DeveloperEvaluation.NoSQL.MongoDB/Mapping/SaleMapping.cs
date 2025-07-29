using Ambev.DeveloperEvaluation.Domain.Entities;
using MongoDB.Bson.Serialization;
using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Mapping;

public class SaleMapping
{
    public SaleMapping()
    {
        BsonClassMap.RegisterClassMap<Sale>(map =>
        {
            map.MapIdProperty(p => p.Id);
            map.MapProperty(p => p.SaleNumber).SetElementName("number");
            map.MapProperty(p => p.Branch).SetElementName("branch");
            map.MapProperty(p => p.User).SetElementName("user");
            map.MapProperty(p => p.TotalAmount).SetElementName("total");
            map.MapProperty(p => p.Status).SetElementName("status");
            map.MapProperty(p => p.CreatedAt).SetElementName("create");
            map.MapProperty(p => p.UpdatedAt).SetElementName("update");
            map.MapProperty(p => p.Items).SetElementName("items");
        });

        BsonClassMap.RegisterClassMap<SaleBranch>(map =>
        {
            map.MapProperty(p => p.Id).SetElementName("id");
            map.MapProperty(p => p.Name).SetElementName("name");
        });

        BsonClassMap.RegisterClassMap<SaleUser>(map =>
        {
            map.MapProperty(p => p.Id).SetElementName("id");
            map.MapProperty(p => p.Name).SetElementName("name");
            map.MapProperty(p => p.City).SetElementName("city");
            map.MapProperty(p => p.State).SetElementName("state");
        });

        BsonClassMap.RegisterClassMap<SaleItem>(map =>
        {
            map.MapProperty(p => p.Id).SetElementName("id");
            map.MapProperty(p => p.Product).SetElementName("product");
            map.MapProperty(p => p.Quantity).SetElementName("quantity");
            map.MapProperty(p => p.Discount).SetElementName("discount");
            map.MapProperty(p => p.Status).SetElementName("status");
            map.MapProperty(p => p.TotalPrice).SetElementName("total");
        });

        BsonClassMap.RegisterClassMap<SaleProduct>(map =>
        {
            map.MapProperty(p => p.Id).SetElementName("id");
            map.MapProperty(p => p.Title).SetElementName("title");
            map.MapProperty(p => p.Price).SetElementName("price");
        });
    }
}
