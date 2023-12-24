using Quesify.SharedKernel.Emailing;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailSender(
        this IServiceCollection services,
        Action<EmailOptions> configureEmailOptions)
    {
        Guard.Against.Null(services, nameof(services));

        EmailOptions emailOptions = new EmailOptions();
        configureEmailOptions(emailOptions);

        return services.AddSingleton(emailOptions);
    }
}
