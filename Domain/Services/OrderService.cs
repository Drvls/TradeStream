using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Enum;
using TradeStream.Domain.Exceptions;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class OrderService(IOrderRepository orderRepository, IAssetService assetService, IUserService userService) : IOrderService
{
    private readonly IOrderRepository _orderRepository =  orderRepository;
    private readonly IAssetService _assetService = assetService;
    private readonly IUserService _userService = userService;
    
    public async Task<OrderResponse> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken)
    {
        AssetResponse assetResponse = await _assetService.GetAssetByCodeAsync(request.AssetCode, cancellationToken);
        if(!assetResponse.IsActive) throw new AssetDisabledException(assetResponse.AssetId, assetResponse.Code);
        
        UserResponse userResponse = await _userService.GetUserAsync(request.UserId, cancellationToken);
        if(!userResponse.IsActive) throw new UserDisabledException(userResponse.Id);
        
        Order order = new Order(
            DateTime.Now,
            request.AssetCode,
            assetResponse.Name,
            request.UserId,
            request.TargetValue,
            request.Quantity,
            request.OrderType
            );

        Order newOrder = await _orderRepository.AddOrderAsync(order, cancellationToken);
        OrderResponse response = new OrderResponse(
            newOrder.Id,
            newOrder.OrderDate,
            newOrder.Status,
            newOrder.AssetCode,
            newOrder.AssetName,
            newOrder.UserId,
            newOrder.TargetValue,
            newOrder.Quantity,
            newOrder.OrderType
        );
        return response;
    }

    public async Task<OrderResponse> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetOrderAsync(orderId, cancellationToken) 
                      ?? throw new OrderNotFoundException(orderId);
        
        OrderResponse response = new OrderResponse(
            order.Id,
            order.OrderDate,
            order.Status,
            order.AssetCode,
            order.AssetName,
            order.UserId,
            order.TargetValue,
            order.Quantity,
            order.OrderType
        );
        return response;
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<Order> orders = await _orderRepository.GetOrdersAsync(page, size, cancellationToken);
        return orders.Select(order => new OrderResponse(
            order.Id,
            order.OrderDate,
            order.Status,
            order.AssetCode,
            order.AssetName,
            order.UserId,
            order.TargetValue,
            order.Quantity,
            order.OrderType
            ));
    }

    public async Task<OrderResponse> CancelOrderAsync(Guid id, CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetOrderAsync(id, cancellationToken) ??  throw new OrderNotFoundException(id);
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

        Order updatedOrder = await _orderRepository.UpdateOrderStatusAsync(order, cancellationToken);
        OrderResponse response = new OrderResponse(
            updatedOrder.Id,
            updatedOrder.OrderDate,
            updatedOrder.Status,
            updatedOrder.AssetCode,
            updatedOrder.AssetName,
            updatedOrder.UserId,
            updatedOrder.TargetValue,
            updatedOrder.Quantity,
            updatedOrder.OrderType
        );
        return response;
    }
}