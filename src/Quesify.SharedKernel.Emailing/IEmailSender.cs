using System.Net.Mail;

namespace Quesify.SharedKernel.Emailing;

public interface IEmailSender
{
    Task SendAsync(MailMessage mailMessage, CancellationToken cancellationToken = default);
}
