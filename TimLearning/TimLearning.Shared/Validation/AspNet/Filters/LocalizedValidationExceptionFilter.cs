using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimLearning.Shared.Validation.AspNet.Mapper;
using TimLearning.Shared.Validation.Exceptions.Localized;

namespace TimLearning.Shared.Validation.AspNet.Filters;

public class LocalizedValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not LocalizedValidationException localizedValidationException)
            return;

        context.Result = new BadRequestObjectResult(
            localizedValidationException.Error.ToValidationErrorResponse()
        );
        context.ExceptionHandled = true;
    }
}
