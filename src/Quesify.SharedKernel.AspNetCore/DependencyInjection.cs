using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quesify.SharedKernel.AspNetCore.Handlers;
using Quesify.SharedKernel.AspNetCore.Logging;
using Quesify.SharedKernel.AspNetCore.Middlewares;
using Quesify.SharedKernel.AspNetCore.Security.Claims;
using Quesify.SharedKernel.AspNetCore.TimeZones;
using Quesify.SharedKernel.Security.Claims;
using Quesify.SharedKernel.Utilities.Guards;
using Quesify.SharedKernel.Utilities.TimeZones;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEnableRequestBuffering(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<EnableRequestBufferingMiddleware>();
    }

    public static IServiceCollection AddRequestTime(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<RequestTimeMiddleware>();
    }

    public static IServiceCollection AddHttpContextCurrentPrincipalAccessor(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
    }

    public static IServiceCollection AddTimeZoneInfoProvider(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<ITimeZoneInfoProvider, TimeZoneInfoProvider>();
    }

    public static IServiceCollection AddPushSerilogProperties(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<PushSerilogPropertiesMiddleware>();
    }

    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
    {
        return services.AddExceptionHandler<CustomExceptionHandler>();
    }

    public static IServiceCollection AddCustomAuthentication(
        this IServiceCollection services,
        string audience,
        string issuer,
        string issuerSigningKey)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.NullOrWhiteSpace(audience, nameof(audience));
        Guard.Against.NullOrWhiteSpace(issuer, nameof(issuer));
        Guard.Against.NullOrWhiteSpace(issuerSigningKey, nameof(issuerSigningKey));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
