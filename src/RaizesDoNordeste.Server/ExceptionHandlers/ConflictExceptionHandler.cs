using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Domain.Exceptions;

namespace RaizesDoNordeste.Server.ExceptionHandlers;

internal sealed class ConflictExceptionHandler(
    IProblemDetailsService problemDetailsService,
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

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflito",
                Detail = exception.Message
            }
        });
    }
}
