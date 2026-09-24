using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Enum;
using TradeStream.Domain.Exceptions;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class OrderService(IOrderRepository orderRepository, IAssetService assetService) : IOrderService
{
    private readonly IOrderRepository _orderRepository =  orderRepository;
    private readonly IAssetService _assetService = assetService;
    
    public async Task<OrderResponse> CreateOrderAsync(OrderRequest order, CancellationToken cancellationToken)
    {
        AssetResponse assetResponse = await _assetService.GetAssetByCodeAsync(order.AssetCode, cancellationToken);
        
        Order newOrder = new Order(
            DateTime.Now,
            order.AssetCode,
            assetResponse.Asset.Name,
            order.UserId,
            order.TargetValue,
            order.Quantity,
            order.OrderType
            );

        return new OrderResponse(await _orderRepository.AddOrderAsync(newOrder, cancellationToken));
    }

    public async Task<OrderResponse> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetOrderAsync(orderId, cancellationToken) 
                      ?? throw new OrderNotFoundException(orderId);
        return new OrderResponse(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<Order> orders = await _orderRepository.GetOrdersAsync(page, size, cancellationToken);
        return orders.Select(order => new OrderResponse(order));
    }

    public async Task<OrderResponse> CancelOrderAsync(Guid id, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetOrderAsync(id, cancellationToken) ??  throw new OrderNotFoundException(id);
        switch (order.Status) {
            case OrderStatus.Cancelled:
                throw new OrderAlreadyCancelledException(id);
                break;
            case OrderStatus.Executed:
                throw new OrderAlreadyExecutedException(id);
                break;
            case OrderStatus.Rejected:
                throw new OrderAlreadyRejectedException(id);
                break;
            default:
                order.Cancel();
                break;
        }

        Order? updatedOrder = await _orderRepository.UpdateOrderStatusAsync(order, cancellationToken);
        return new OrderResponse(updatedOrder);
    }
}