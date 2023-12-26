using MongoDB.Driver;
using Quesify.SharedKernel.MongoDB.Models;
using System.Text.Json;

namespace Quesify.SharedKernel.MongoDB.Contexts;

public class MongoDbContext : IMongoDbContext
{
    protected MongoClient MongoClient { get; }

    protected IMongoDatabase MongoDatabase { get; }

    protected MongoDbOptions MongoDbOptions { get; }

    public MongoDbContext(MongoDbOptions mongoDbOptions)
    {
        MongoClient = new MongoClient(mongoDbOptions.ConnectionString);
        MongoDatabase = MongoClient.GetDatabase(mongoDbOptions.Database);
        MongoDbOptions = mongoDbOptions;
    }

    public IMongoCollection<T> Set<T>(string collectionName = "")
    {
        return MongoDatabase.GetCollection<T>(GetCollectionName<T>(collectionName));
    }

    private string GetCollectionName<T>(string collectionName)
    {
        var collection = collectionName.IsNullOrWhiteSpace()
            ? $"{JsonNamingPolicy.CamelCase.ConvertName(typeof(T).Name).Pluralize()}"
            : collectionName;
        var schema = MongoDbOptions.Schema;
        return schema.IsNullOrWhiteSpace() ? collection : $"{schema}.{collection}";
    }
}
