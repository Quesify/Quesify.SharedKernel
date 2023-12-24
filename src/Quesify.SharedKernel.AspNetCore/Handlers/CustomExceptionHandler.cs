﻿using Quesify.SharedKernel.Utilities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Quesify.SharedKernel.Validation;

namespace Quesify.SharedKernel.AspNetCore.Handlers;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message.IsNullOrEmpty() ? null : exception.Message;

        switch (exception)
        {
            case ValidationFailureException validationFailureException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteValidationFailureProblemDetailsAsJsonAsync(exceptionMessage ?? ErrorMessages.ValidationFailureErrorMessage, validationFailureException.Errors);
                break;
            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteUnauthorizedAccessProblemDetailsAsJsonAsync(exceptionMessage ?? ErrorMessages.UnauthorizedAccessErrorMessage);
                break;
            case ForbiddenAccessException:
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await httpContext.Response.WriteForbiddenAccessProblemDetailsAsJsonAsync(exceptionMessage ?? ErrorMessages.ForbiddenAccessErrorMessage);
                break;
            case NotFoundException:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteNotFoundProblemDetailsAsJsonAsync(exceptionMessage ?? ErrorMessages.NotFoundErrorMessage);
                break;
            case BusinessException businessException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteBusinessProblemDetailsAsJsonAsync(exceptionMessage ?? ErrorMessages.BusinessErrorMessage, businessException.Errors);
                break;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteInternalServerErrorProblemDetailsAsJsonAsync(ErrorMessages.InternalServerErrorMessage);
                break;
        }

        return true;
    }
}
