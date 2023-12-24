using Quesify.SharedKernel.Security.Claims;
using System.Security.Claims;

namespace Quesify.SharedKernel.AspNetCore.Security.Claims;

public class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor
{
    public ClaimsPrincipal ClaimsPrincipal { get; }

    public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal = httpContextAccessor.HttpContext?.User ?? (Thread.CurrentPrincipal as ClaimsPrincipal)!;
    }
}
