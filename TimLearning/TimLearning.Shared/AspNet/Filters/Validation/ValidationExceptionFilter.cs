using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TimLearning.Shared.AspNet.Filters.Validation;

public class ValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            var errorDetail = new ErrorDetail(
                validationException.Message,
                validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())
            );

            context.Result = new BadRequestObjectResult(errorDetail);
            context.ExceptionHandled = true;
        }
    }
}
