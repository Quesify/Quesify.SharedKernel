namespace Quesify.SharedKernel.Security.Tokens;

public class AccessTokenOptions
{
    public string Audience { get; set; }

    public string Issuer { get; set; }

    public int Expiration { get; set; }

    public string SecurityKey { get; set; }

    public AccessTokenOptions()
    {
        Audience = null!;
        Issuer = null!;
        SecurityKey = null!;
    }
}
