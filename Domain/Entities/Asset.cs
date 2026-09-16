namespace TradeStream.Domain.Entities;

public class Asset
{
    public Guid AssetId { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public List<decimal> PriceHistory { get; set; } = [];
    public int Quantity { get; set; } = 0;
}