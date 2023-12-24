﻿using Microsoft.AspNetCore.Mvc;

namespace Quesify.SharedKernel.AspNetCore.ObjectResults;

public class ForbiddenAccessObjectResult : ObjectResult
{
    public ForbiddenAccessObjectResult(object? value) 
        : base(value)
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
}
