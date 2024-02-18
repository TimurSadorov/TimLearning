using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TimLearning.Shared.AspNet.Swagger.Filters.Validation;

public static class SwaggerGenOptionsConfiguration
{
    public static SwaggerGenOptions ConfigureValidationErrorOperation(
        this SwaggerGenOptions options
    )
    {
        options.DocumentFilter<ValidationErrorDocumentFilter>();
        options.OperationFilter<ValidationErrorOperationFilter>();

        return options;
    }
}
