using Microsoft.EntityFrameworkCore;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;
using TradeStream.Infra.Database;

namespace TradeStream.Infra.Repositories;

public class OrderRepository(TradeDbContext tradeDbContext) : IOrderRepository
{
    private readonly TradeDbContext _tradeDbContext = tradeDbContext;
    
    public async Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _tradeDbContext.Orders.Add(order);
        await _tradeDbContext.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        Order? order = await _tradeDbContext.Orders.FindAsync(orderId, cancellationToken);
        return order;
    }

    public async Task<IEnumerable<Order?>> GetOrdersAsync(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<Order> orders = await _tradeDbContext.Orders
            .Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
        return orders;
    }

    public async Task<Order> UpdateOrderStatusAsync(Order updatedOrder, CancellationToken cancellationToken)
    {
        _tradeDbContext.Orders.Update(updatedOrder);
        await _tradeDbContext.SaveChangesAsync(cancellationToken);
        return updatedOrder;
    }
}