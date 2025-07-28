using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MongoDB.Bson;

namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB;

public class MongoDbIDGenerator : IIDGenerator
{
    public string Generate() => ObjectId.GenerateNewId().ToString();
}
