using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken);
}