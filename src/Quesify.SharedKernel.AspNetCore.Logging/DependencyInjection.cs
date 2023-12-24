using Quesify.SharedKernel.AspNetCore.Logging.Middlewares;
using Quesify.SharedKernel.AspNetCore.Logging.Models;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomHttpLogging(
        this IServiceCollection services,
        Action<LoggingOptions> configureLoggingOptions)
    {
        Guard.Against.Null(services, nameof(services));

        LoggingOptions loggingOptions = new LoggingOptions();
        configureLoggingOptions(loggingOptions);    

        return services
            .AddSingleton(loggingOptions)
            .AddScoped<CustomHttpLoggingMiddleware>();
    }
}
