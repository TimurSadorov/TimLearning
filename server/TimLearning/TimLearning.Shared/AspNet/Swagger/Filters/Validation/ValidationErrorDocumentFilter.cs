using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TimLearning.Shared.AspNet.Validations.Dto;

namespace TimLearning.Shared.AspNet.Swagger.Filters.Validation;

public class ValidationErrorDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Components.Schemas.Add(
            nameof(ValidationErrorResponse),
            new OpenApiSchema
            {
                OneOf = new List<OpenApiSchema>
                {
                    context.SchemaGenerator.GenerateSchema(
                        typeof(ModelValidationErrorResponse),
                        context.SchemaRepository
                    ),
                    context.SchemaGenerator.GenerateSchema(
                        typeof(ValidationErrorTextResponse),
                        context.SchemaRepository
                    )
                }
            }
        );
    }
}
