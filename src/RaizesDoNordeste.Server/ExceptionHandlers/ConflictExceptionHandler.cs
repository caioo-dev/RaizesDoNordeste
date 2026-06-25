using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Domain.Exceptions;

namespace RaizesDoNordeste.Server.ExceptionHandlers;

internal sealed class ConflictExceptionHandler(
    ILogger<ConflictExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ConflictException)
        {
            return false;
        }

        logger.LogWarning(exception, "Conflito de recurso");

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Conflito",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}
