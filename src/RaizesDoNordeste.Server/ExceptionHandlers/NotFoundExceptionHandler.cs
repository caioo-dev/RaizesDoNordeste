using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.CrossCutting.Exceptions;

namespace RaizesDoNordeste.Server.ExceptionHandlers;

internal sealed class NotFoundExceptionHandler(
    ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
        )
    {
        if (exception is not NotFoundException)
        {
            return false;
        }

        logger.LogWarning(exception, "Recurso não encontrado");

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Recurso não encontrado",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}
