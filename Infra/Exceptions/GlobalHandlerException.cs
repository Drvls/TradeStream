using Microsoft.AspNetCore.Diagnostics;
using TradeStream.Application.DTOs;
using TradeStream.Domain.Exceptions;

namespace TradeStream.Infra.Exceptions;

public class GlobalHandlerException : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case AssetNotFoundException:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Error = "Asset not found",
                        Message = exception.Message
                    }
                );
                
                break;
            
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }
        
        return ValueTask.FromResult(true);
    }
}