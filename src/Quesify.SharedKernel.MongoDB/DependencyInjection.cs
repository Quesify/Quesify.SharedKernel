using Quesify.SharedKernel.MongoDB.Contexts;
using Quesify.SharedKernel.MongoDB.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDB<TDbContext>(
        this IServiceCollection services,
        Action<MongoDbOptions> configureMongoDbOptions)
        where TDbContext : MongoDbContext
    {
        MongoDbOptions mongoDbOptions = new MongoDbOptions();
        configureMongoDbOptions(mongoDbOptions);

        return services
            .AddScoped<TDbContext>()
            .AddSingleton(mongoDbOptions);
    }
}

