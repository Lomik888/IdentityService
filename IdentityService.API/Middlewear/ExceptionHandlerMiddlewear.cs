using Microsoft.AspNetCore.Diagnostics;

namespace IdentityService.API.Middlewear;

public class ExceptionHandlerMiddlewear : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}