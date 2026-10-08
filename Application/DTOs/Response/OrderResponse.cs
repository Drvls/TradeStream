using TradeStream.Domain.Entities;
using TradeStream.Domain.Enum;

namespace TradeStream.Application.DTOs.Response;

public record OrderResponse(
    Guid Id,
    DateTime OrderDate,
    OrderStatus Status,
    string AssetCode,
    string AssetName,
    Guid UserId,
    decimal TargetValue,
    int Quantity,
    OrderType OrderType)
{
    public Guid Id { get; set; } = Id;
    public DateTime OrderDate { get; set; } = OrderDate;
    public OrderStatus Status { get; set; } = Status;
    public string AssetCode { get; set; } = AssetCode;
    public string AssetName { get; set; } = AssetName;
    public Guid UserId { get; set; } = UserId;
    public decimal TargetValue { get; set; } = TargetValue;
    public int Quantity { get; set; } = Quantity;
    public OrderType OrderType { get; set; } = OrderType;
}