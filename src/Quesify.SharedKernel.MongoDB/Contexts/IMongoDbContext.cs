using MongoDB.Driver;

namespace Quesify.SharedKernel.MongoDB.Contexts;

public interface IMongoDbContext
{
    IMongoCollection<T> Set<T>(string collectionName = "");
}

