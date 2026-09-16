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
}