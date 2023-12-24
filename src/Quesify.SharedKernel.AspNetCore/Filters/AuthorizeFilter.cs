using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Quesify.SharedKernel.AspNetCore.HttpProblemDetails;
using Quesify.SharedKernel.AspNetCore.ObjectResults;
using Quesify.SharedKernel.Security.Users;

namespace Quesify.SharedKernel.AspNetCore.Filters;

public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private string[] _roles;

    public AuthorizeAttribute(string[]? roles = null)
    {
        _roles = roles ?? [];
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var currentUser = context.HttpContext.RequestServices.GetRequiredService<ICurrentUser>();

        if (!currentUser.IsAuthenticated)
        {
            context.Result =
               new UnauthorizedObjectResult(
                   new UnauthorizedAccessProblemDetails(
                       ErrorMessages.UnauthorizedAccessErrorMessage,
                       context.HttpContext.Request.Path
                    )
                );
            return;
        }

        if (_roles.Any() && !currentUser.IsInRoles(_roles))
        {
            context.Result =
                new ForbiddenAccessObjectResult(
                    new ForbiddenAccessProblemDetails(
                        ErrorMessages.ForbiddenAccessErrorMessage,
                        context.HttpContext.Request.Path
                    )
                );
            return;
        }

        await Task.CompletedTask;
    }
}
