using Quesify.SharedKernel.MongoDB.Contexts;

namespace Quesify.SharedKernel.MongoDB.Models;

public class MongoDbOptions
{
    public string ConnectionString { get; set; }

    public string Database { get; set; }

    public string Schema { get; set; }

    public MongoDbOptions()
    {
        ConnectionString = string.Empty;
        Database = string.Empty;
        Schema = string.Empty;
    }
}

public class MongoDbOptions<DbContext> : MongoDbOptions
    where DbContext : MongoDbContext
{
}
