using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimLearning.Shared.Validation.AspNet.Dto;
using TimLearning.Shared.Validation.Exceptions;

namespace TimLearning.Shared.Validation.AspNet.Filters;

public class ValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not LocalizedValidationException localizedValidationException)
            return;

        var errorDetail = new ValidationErrorDetail(
            localizedValidationException.PropertiesErrors.ToDictionary(p => p.Name, p => p.Errors)
        );

        context.Result = new BadRequestObjectResult(errorDetail);
        context.ExceptionHandled = true;
    }
}
