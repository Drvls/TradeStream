using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;
using TradeStream.Infra.Database;

namespace TradeStream.Infra.Repositories;

public class PositionRepository(TradeDbContext tradeDbContext) : IPositionRepository
{
    private readonly TradeDbContext _tradeDbContext = tradeDbContext;
    
    public async Task AddPositionAsync(Position position, CancellationToken cancellationToken)
    {
        _tradeDbContext.Positions.Add(position);
        await _tradeDbContext.SaveChangesAsync(cancellationToken);
    }
}