using TradeStream.Domain.Entities;

namespace TradeStream.Application.DTOs.Response;

public record AssetResponse(
    Guid AssetId,
    string Code,
    string Name,
    decimal Price,
    List<decimal> PriceHistory,
    int Quantity,
    bool IsActive)
{
    public Guid AssetId { get; set; } = AssetId;
    public string Code { get; set; } = Code;
    public string Name { get; set; } = Name;
    public decimal Price { get; set; } = Price;
    public List<decimal> PriceHistory { get; set; } = PriceHistory;
    public int Quantity { get; set; } = Quantity;
    public bool IsActive { get; set; } = IsActive;
}