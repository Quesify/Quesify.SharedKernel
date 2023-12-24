using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Emailing.Mailkit;

namespace Quesify.SharedKernel.Emailing;

public static class EmailOptionsExtensions
{
    public static EmailOptions UseMailKit(
        this EmailOptions emailOptions,
        IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, MailkitEmailSender>();
        return emailOptions;
    }
}
