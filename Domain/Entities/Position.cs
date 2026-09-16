namespace TradeStream.Domain.Entities;

public class Position
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid AssetId { get; init; }
    public int Quantity { get; set; }
    
    // Navigation Property
    public User User { get; set; }
    public Asset Asset { get; set; }
}