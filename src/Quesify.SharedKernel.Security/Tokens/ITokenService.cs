namespace Quesify.SharedKernel.Security.Tokens;

public interface ITokenService
{
    AccessToken CreateAccessToken(AccessTokenClaims accessTokenClaims);
}
