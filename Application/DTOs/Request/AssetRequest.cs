namespace TradeStream.Application.DTOs.Request;

public record AssetRequest
{
    public string Code { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}