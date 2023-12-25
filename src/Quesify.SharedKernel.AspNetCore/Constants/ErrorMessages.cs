﻿namespace Quesify.SharedKernel.AspNetCore;

public static class ErrorMessages
{
    public const string ValidationFailureErrorMessage = "One or more validation failures have occurred.";
    public const string UnauthorizedAccessErrorMessage = "Authorization has been denied for this request.";
    public const string ForbiddenAccessErrorMessage = "You do not have permission to access this resource.";
    public const string NotFoundErrorMessage = "The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.";
    public const string BusinessErrorMessage = "Your request has encountered obstacles due to errors in the submitted data.";
    public const string InternalServerErrorMessage = "The server encountered an internal error or misconfiguration and was unable to complete your request.";
}
