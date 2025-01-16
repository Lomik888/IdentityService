using Microsoft.AspNetCore.Diagnostics;

namespace IdentityService.API.Middlewear;

public class ExceptionHandlerMiddlewear : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (!httpContext.Response.HasStarted)
        {
            var response = new
            {
                message = "Попробуй ещё раз",
                Date = DateTime.UtcNow,
                StatusCode = StatusCodes.Status400BadRequest,
                IsSuccess = false,
                ErrorMessage = exception.Message
            };

            httpContext.Response.StatusCode = 400;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);

            return true;
        }

        return false;
    }
}