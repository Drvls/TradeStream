using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken);
    Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetOrdersAsync(int page, int size, CancellationToken cancellationToken);
    Task<Order?> UpdateOrderStatusAsync(Order order, CancellationToken cancellationToken);
}