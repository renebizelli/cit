using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MongoDB.Bson;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Services;

public class MongoDbStringIDGenerator : IStringIDGenerator
{
    public string Generate() => ObjectId.GenerateNewId().ToString();
}
