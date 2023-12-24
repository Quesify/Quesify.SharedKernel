using Microsoft.AspNetCore.Mvc;

namespace Quesify.SharedKernel.AspNetCore.HttpProblemDetails;

public class BusinessProblemDetails : ProblemDetails
{
    public BusinessProblemDetails(string? detail, string? instance, object? errors)
    {
        Title = "Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status422UnprocessableEntity;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Instance = instance;
        if (errors != null) Extensions.Add("errors", errors);
    }
}
