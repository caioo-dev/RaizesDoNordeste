using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.CrossCutting.Exceptions;

namespace RaizesDoNordeste.Server.ExceptionHandlers;

internal sealed class UnauthorizedExceptionHandler(
    ILogger<UnauthorizedExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedException)
        {
            return false;
        }
    
        logger.LogWarning(exception, "Tentativa de autenticação inválida");

        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Não autorizado",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}
