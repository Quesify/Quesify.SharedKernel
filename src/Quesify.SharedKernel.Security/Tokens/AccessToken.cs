namespace Quesify.SharedKernel.Security.Tokens;

public class AccessToken
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }

    public AccessToken()
    {
        Token = string.Empty;
    }
}
