using Quesify.SharedKernel.Utilities.TimeProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quesify.SharedKernel.Security.Tokens;

public class JwtService : ITokenService
{
    private readonly AccessTokenOptions _accessTokenOptions;
    private readonly IDateTime _dateTime;

    public JwtService(
        AccessTokenOptions accessTokenOptions,
        IDateTime dateTime)
    {
        _accessTokenOptions = accessTokenOptions;
        _dateTime = dateTime;
    }

    public AccessToken CreateAccessToken(AccessTokenClaims accessTokenClaims)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenOptions.SecurityKey));
        
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        DateTime expires = _dateTime.Now.AddMinutes(_accessTokenOptions.Expiration);
       
        JwtSecurityToken accessToken = new JwtSecurityToken(
            audience: _accessTokenOptions.Audience,
            issuer: _accessTokenOptions.Issuer,
            expires: expires,
            notBefore: _dateTime.Now,
            signingCredentials: signingCredentials,
            claims: GetClaims(accessTokenClaims));
      
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      
        return new AccessToken()
        {
            Token = tokenHandler.WriteToken(accessToken),
            Expiration = expires
        };
    }

    private List<Claim> GetClaims(AccessTokenClaims accessTokenClaims)
    {
        List<Claim> claims = new List<Claim>();

        if (accessTokenClaims.UserId != null)
        {
            claims.Add(CreateClaim(ClaimTypes.NameIdentifier, accessTokenClaims.UserId.ToString()!));
        }

        if (accessTokenClaims.UserName != null)
        {
            claims.Add(CreateClaim(ClaimTypes.Name, accessTokenClaims.UserName));
        }

        if (accessTokenClaims.Email != null)
        {
            claims.Add(CreateClaim(ClaimTypes.Email, accessTokenClaims.Email.ToString()!));
        }

        if (accessTokenClaims.PhoneNumber != null)
        {
            claims.Add(CreateClaim(ClaimTypes.MobilePhone, accessTokenClaims.PhoneNumber.ToString()!));
        }

        if (accessTokenClaims.Roles.Length > 0)
        {
            foreach (var role in accessTokenClaims.Roles)
            {
                claims.Add(CreateClaim(ClaimTypes.Role, role));
            }
        }

        return claims;
    }

    private Claim CreateClaim(string type, string value)
    {
        return new Claim(type, value);
    }
}
