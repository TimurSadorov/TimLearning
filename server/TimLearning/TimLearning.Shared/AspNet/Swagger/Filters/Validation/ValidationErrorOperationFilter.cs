using System.Net.Mime;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TimLearning.Shared.AspNet.Validations.Dto;

namespace TimLearning.Shared.AspNet.Swagger.Filters.Validation;

public class ValidationErrorOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add(
            "422",
            new OpenApiResponse
            {
                Description = "Request validation error.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        MediaTypeNames.Application.Json,
                        new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.Schema,
                                    Id = nameof(ValidationErrorResponse)
                                }
                            }
                        }
                    }
                }
            }
        );
    }
}