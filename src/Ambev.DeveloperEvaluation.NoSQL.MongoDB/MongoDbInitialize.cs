using Ambev.DeveloperEvaluation.NoSQL.MongoDB.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB;

public class MongoDbInitialize
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var pack = new ConventionPack();
        pack.Add(new CamelCaseElementNameConvention());
        ConventionRegistry.Register("Camel Case Convention", pack, t => true);
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        _ = new CartMapping(); 
        _ = new ProductMapping();

        var settings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>();

        ArgumentNullException.ThrowIfNull(settings);

        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(settings.ConnectionString);
        });

        builder.Services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DataBaseName);
        });
    }
}
