using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TimLearning.Shared.App.Exceptions;

namespace TimLearning.Shared.App.AspNet.Middlewares;

public class AppExceptionMiddleware<TValidationError>
where TValidationError: Enum 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionMiddleware<TValidationError>> _logger;
    private readonly string _path;

    public AppExceptionMiddleware(
        RequestDelegate next,
        ILogger<AppExceptionMiddleware<TValidationError>> logger,
        string path
    )
    {
        _next = next;
        _path = path;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IHostEnvironment env)
    {
        if (
            context.Request.Path.StartsWithSegments(
                _path,
                StringComparison.CurrentCultureIgnoreCase
            )
        )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, env, ex);
            }
        }
        else
        {
            await _next(context);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, IHostEnvironment env, Exception exception)
    {
        HttpStatusCode statusCode;
        string contentType;
        string content;

        if (exception is AppValidationException<TValidationError> validationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            contentType = MediaTypeNames.Application.Json;
            content = new ValidationErrorDetail(validationException.Error, validationException.Message, validationException.PropertiesErrors.GroupBy(validationResult => validationResult.Name).ToDictionary(propertyGroup => propertyGroup.Key, propertyGroup => propertyGroup.Select(p => p.Errors)));
            validationException.Log(_logger);
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            contentType = System.Net.Mime.MediaTypeNames.Application.Json;
            var errorDto = new ApiExceptionDto
            {
                Message = exception.Message.HasText() ? exception.Message : null,
                ConnectionId = context.Connection.Id
            };
            if (env.IsDevelopment())
            {
                errorDto.StackTrace = exception.StackTrace ?? null;
                errorDto.InnerException = exception.InnerException?.StackTrace ?? null;
            }

            content = JsonSerializer.Serialize(
                errorDto,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                }
            );

            _logger.LogError(exception, "ApiExceptionMiddleware: {Message}", exception.Message);
        }

        context.Response.ContentType = contentType;
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(content);
    }
}
