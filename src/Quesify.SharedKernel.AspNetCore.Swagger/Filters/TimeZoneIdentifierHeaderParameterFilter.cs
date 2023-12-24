using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Quesify.SharedKernel.AspNetCore.Swagger.Filters;

public class TimeZoneIdentifierHeaderParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Time-Zone-Identifier",
            In = ParameterLocation.Header,
            Description = "Time Zone Identifier Header Parameter",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("UTC")
            }
        });
    }
}
