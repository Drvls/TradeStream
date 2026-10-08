using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Controllers;

[ApiController]
[Route("tradestream/[controller]")]
public class OrderController(IOrderService orderService, IValidator<OrderRequest> orderValidator) : ControllerBase
{
    private readonly IValidator<OrderRequest> _orderValidator = orderValidator;
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _orderValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            
        OrderResponse response = await _orderService.CreateOrderAsync(request, cancellationToken);
        return Created($"/users/{response.UserId}/orders/{response.Id}", response);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        OrderResponse response = await _orderService.GetOrderAsync(orderId, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IEnumerable<OrderResponse>> GetOrders(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<OrderResponse> orders = await _orderService.GetOrdersAsync(page, size, cancellationToken);
        return orders;
    }

    [HttpPatch("{orderId:guid}")]
    public async Task<IActionResult> CancelOrder(Guid orderId, CancellationToken cancellationToken)
    {
        OrderResponse order = await _orderService.CancelOrderAsync(orderId, cancellationToken);
        return Ok(order);
    }
}