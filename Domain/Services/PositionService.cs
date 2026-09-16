using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class PositionService(IPositionRepository positionRepository) : IPositionService
{
    private readonly IPositionRepository _positionRepository =  positionRepository;
    
    public async Task CreatePositionAsync(Position position, CancellationToken cancellationToken)
    {
        await _positionRepository.AddPositionAsync(position, cancellationToken);
    }
}