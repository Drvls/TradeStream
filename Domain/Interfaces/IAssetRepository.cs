using TradeStream.Domain.Entities;

namespace TradeStream.Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetAssetByCodeAsync(string code, CancellationToken cancellationToken);
    Task<Asset?> FindAssetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Asset> AddAssetAsync(Asset asset, CancellationToken cancellationToken);
    Task<Asset> UpdateAssetAsync(Asset asset, CancellationToken cancellationToken);
    Task<IEnumerable<Asset>> GetAssetsAsync(int page, int size, CancellationToken cancellationToken);
    Task UpdateAssetActivityStatusAsync(Asset asset, CancellationToken cancellationToken);
}