using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro processando a requisição {Method} {Path}", context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message;
        object? errors = null;

        switch (exception)
        {
            case ValidationException validationEx:
                status = HttpStatusCode.BadRequest;
                message = "Erro de validação dos dados";
                errors = validationEx.Errors; 
                break;
            case KeyNotFoundException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                message = "Ocorreu um erro interno no servidor.";
                break;
        }

        var response = new ErrorResponse
        {
            Status = (int)status,
            Message = message,
            Errors = errors
        };

        var payload = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(payload);
    }
}
