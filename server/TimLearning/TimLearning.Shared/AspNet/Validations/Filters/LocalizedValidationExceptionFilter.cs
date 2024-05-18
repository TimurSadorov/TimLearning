using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimLearning.Shared.AspNet.Validations.Mappers;
using TimLearning.Shared.Validation.Exceptions.Localized;

namespace TimLearning.Shared.AspNet.Validations.Filters;

public class LocalizedValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not LocalizedValidationException localizedValidationException)
            return;

        context.Result = new UnprocessableEntityObjectResult(
            localizedValidationException.Error.ToValidationErrorResponse()
        );
        context.ExceptionHandled = true;
    }
}
