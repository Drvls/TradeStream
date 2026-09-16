using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;

namespace TradeStream.Domain.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(OrderRequest order, CancellationToken cancellationToken);
}