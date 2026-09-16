using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IPositionRepository
{
    Task AddPositionAsync(Position position, CancellationToken cancellationToken);
}