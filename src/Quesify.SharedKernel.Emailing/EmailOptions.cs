namespace Quesify.SharedKernel.Emailing;

public class EmailOptions
{
    public string Host { get; set; }

    public int Port { get; set; }

    public bool EnableSsl { get; set; }

    public string SenderFullName { get; set; }

    public string SenderEmail { get; set; }

    public string Password { get; set; }

    public EmailOptions()
    {
        Host = null!;
        SenderFullName = null!;
        SenderEmail = null!;
        Password = null!;
    }
}
