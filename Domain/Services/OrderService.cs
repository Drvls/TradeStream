using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
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
            order.Quantity
            );

        return new OrderResponse(await _orderRepository.AddOrderAsync(newOrder, cancellationToken));
    }
}