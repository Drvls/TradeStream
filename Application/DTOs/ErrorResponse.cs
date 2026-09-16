namespace TradeStream.Application.DTOs;

public record ErrorResponse
{
    public int StatusCode { get; init; }
    public string Error { get; init; }
    public string Message { get; init; }
}