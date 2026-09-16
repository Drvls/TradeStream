using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> FindByCodeAsync(string code, CancellationToken cancellationToken);
}