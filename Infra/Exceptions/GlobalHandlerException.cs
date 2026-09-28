using Microsoft.AspNetCore.Diagnostics;
using TradeStream.Application.DTOs;
using TradeStream.Domain.Exceptions;

namespace TradeStream.Infra.Exceptions;

public class GlobalHandlerException : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case AssetNotFoundException:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Error = "Asset not found",
                        Message = exception.Message
                    }, cancellationToken
                    );
                break;
            
            case OrderNotFoundException:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Error = "Order not found",
                        Message = exception.Message
                    }, cancellationToken
                );
                break;
            
            case OrderAlreadyExecutedException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Error = "Order already executed",
                        Message = exception.Message
                    }, cancellationToken
                );
                break;
            
            case OrderAlreadyRejectedException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Error = "Order already rejected",
                        Message = exception.Message
                    }, cancellationToken
                );
                break;
            
            case OrderAlreadyCancelledException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteAsJsonAsync(
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status409Conflict,
                        Error = "Order already cancelled",
                        Message = exception.Message
                    }, cancellationToken
                );
                break;
            
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }
        
        return await ValueTask.FromResult(true);
    }
}