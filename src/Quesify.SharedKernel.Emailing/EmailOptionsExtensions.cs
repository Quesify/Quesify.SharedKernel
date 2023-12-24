using Microsoft.Extensions.DependencyInjection;

namespace Quesify.SharedKernel.Emailing;

public static class EmailOptionsExtensions
{
    public static EmailOptions UseFakeEmailSender(
        this EmailOptions emailOptions,
        IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, FakeEmailSender>();
        return emailOptions;
    }
}
