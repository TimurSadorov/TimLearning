using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimLearning.Domain.Exceptions;

namespace TimLearning.Api.Filters;

public class NotFoundExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not NotFoundException)
            return;

        context.Result = new NotFoundResult();
        context.ExceptionHandled = true;
    }
}
