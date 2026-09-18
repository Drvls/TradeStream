using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(OrderRequest order, CancellationToken cancellationToken);
    Task<OrderResponse> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponse>> GetOrdersAsync(int page, int size, CancellationToken cancellationToken);
}