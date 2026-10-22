namespace TradeStream.Application.DTOs.Response;

public record PositionResponse(Guid Id, Guid AssetId, int Quantity)
{
    public Guid Id { get; init; } = Id;
    public Guid AssetId { get; init; } = AssetId;
    public int Quantity { get; init; } = Quantity;
}