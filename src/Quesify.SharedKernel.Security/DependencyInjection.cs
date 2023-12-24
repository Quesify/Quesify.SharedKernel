using Quesify.SharedKernel.Security.Tokens;
using Quesify.SharedKernel.Security.Users;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services,
        Action<AccessTokenOptions> configureAccessTokenOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureAccessTokenOptions, nameof(configureAccessTokenOptions));

        AccessTokenOptions accessTokenOptions = new AccessTokenOptions();
        configureAccessTokenOptions(accessTokenOptions);

        return services
            .AddSingleton(accessTokenOptions)
            .AddSingleton<ITokenService, JwtService>()
            .AddScoped<ICurrentUser, CurrentUser>();
    }
}
