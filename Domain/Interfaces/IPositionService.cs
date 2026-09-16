using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IPositionService
{
    Task CreatePositionAsync(Position position, CancellationToken cancellationToken);
}