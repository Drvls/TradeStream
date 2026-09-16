using TradeStream.Domain.Enum;

namespace TradeStream.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OrderDate { get; set; } = default;
    public OrderStatus Status { get; set; }
    public string AssetCode { get; set; } = default!;
    public string AssetName { get; set; } = default!;
    public Guid UserId { get; set; } = default;
    public decimal TargetValue { get; set; } = default;
    public int Quantity { get; set; } = default;

    // Navigation property
    public User User { get; set; } 
    
    public Order(DateTime orderDate, string assetCode, string assetName, Guid userId, decimal targetValue, int quantity)
    {
        OrderDate = orderDate;
        Status = OrderStatus.Pending;
        AssetCode = assetCode;
        AssetName = assetName;
        UserId = userId;
        TargetValue = targetValue;
        Quantity = quantity;
    }
}