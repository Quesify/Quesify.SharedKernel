using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Quesify.SharedKernel.AspNetCore.Swagger.Filters;

public class AcceptLanguageHeaderParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Description = "Language Preference For The Response",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("tr-TR")
            }
        });
    }
}
