using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimLearning.Domain.Exceptions;

namespace TimLearning.Api.Filters;

public class AccessExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not AccessException)
            return;

        context.Result = new ForbidResult();
        context.ExceptionHandled = true;
    }
}
