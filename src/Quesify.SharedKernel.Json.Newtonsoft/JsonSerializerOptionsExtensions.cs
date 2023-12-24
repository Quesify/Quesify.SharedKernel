using Quesify.SharedKernel.Json.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Quesify.SharedKernel.Json;

public static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions UseNewtonsoft(
        this JsonSerializerOptions jsonSerializerOptions,
        IServiceCollection services,
        Action<JsonSerializerSettings> configureJsonSerializerSettings)
    {
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        configureJsonSerializerSettings(jsonSerializerSettings);

        services.AddSingleton(jsonSerializerSettings);
        services.AddSingleton<IJsonSerializer, NewtonsoftSerializer>();

        return jsonSerializerOptions;
    }
}
