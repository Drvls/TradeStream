using TradeStream.Domain.Entities;

namespace TradeStream.Application.DTOs.Response;

public record UserResponse(
    Guid Id,
    decimal Balance,
    List<Position> Positions,
    List<Order> Orders,
    bool IsActive)
{
    public Guid Id { get; set; } = Id;
    public decimal Balance  { get; set; } = Balance;
    public List<Position> Positions { get; set; } = Positions;
    public List<Order> Orders { get; set; } = Orders;
    public bool IsActive { get; set; } = IsActive;
}