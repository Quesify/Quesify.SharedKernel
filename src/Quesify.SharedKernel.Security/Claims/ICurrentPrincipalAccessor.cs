using System.Security.Claims;

namespace Quesify.SharedKernel.Security.Claims;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal ClaimsPrincipal { get; }
}
