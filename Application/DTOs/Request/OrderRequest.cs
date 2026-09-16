namespace TradeStream.Application.DTOs.Request;

public record OrderRequest
{
    public string AssetCode { get; init; }
    public Guid UserId { get; init; }
    public decimal TargetValue { get; init; }
    public int Quantity { get; init; }
    
}