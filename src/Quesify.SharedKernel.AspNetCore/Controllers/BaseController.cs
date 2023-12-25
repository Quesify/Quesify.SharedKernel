using Microsoft.AspNetCore.Mvc;
using Quesify.SharedKernel.AspNetCore.Constants;
using Quesify.SharedKernel.AspNetCore.Models;

namespace Quesify.SharedKernel.AspNetCore.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult OkResponse(string? detail = null, object? data = null)
    {
        return Ok(new ApiResponse()
        {
            Title = "Ok",
            Detail = detail ?? ResponseMessages.OkResponseMessage,
            Status = StatusCodes.Status200OK,
            Instance = HttpContext.Request.Path,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
            Data = data
        });
    }

    protected IActionResult CreatedResponse(string? detail = null, object? data = null)
    {
        return Created("", new ApiResponse()
        {
            Title = "Created",
            Detail = detail ?? ResponseMessages.CreatedResponse,
            Status = StatusCodes.Status201Created,
            Instance = HttpContext.Request.Path,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.2",
            Data = data
        });
    }
}
