using Quesify.SharedKernel.AspNetCore.Swagger.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerGenOptionsExtensions
{
    public static void AddSecurity(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }});
    }

    public static void AddAcceptLanguageHeaderParameterFilter(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.OperationFilter<AcceptLanguageHeaderParameterFilter>();
    }

    public static void AddTimeZoneIdentifierHeaderParameterFilter(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.OperationFilter<TimeZoneIdentifierHeaderParameterFilter>();
    }

    public static void AddLowercaseDocumentFilter(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.DocumentFilter<LowercaseDocumentFilter>();
    }
}
