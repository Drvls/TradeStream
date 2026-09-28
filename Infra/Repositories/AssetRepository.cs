using Microsoft.EntityFrameworkCore;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Interfaces;
using TradeStream.Infra.Database;

namespace TradeStream.Infra.Repositories;

public class AssetRepository(TradeDbContext tradeDbContext) : IAssetRepository
{
    private readonly TradeDbContext _tradeDbContext = tradeDbContext;
    
    public async Task<Asset?> GetAssetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        Asset? asset = await _tradeDbContext.Assets.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        return asset;
    }

    public async Task<Asset?> FindAssetByIdAsync(Guid assetId, CancellationToken cancellationToken)
    {
        Asset? asset = await _tradeDbContext.Assets.FindAsync(assetId, cancellationToken);
        return asset;
    }

    public async Task<Asset?> AddAssetAsync(Asset asset, CancellationToken cancellationToken)
    {
        _tradeDbContext.Assets.Add(asset);
        await _tradeDbContext.SaveChangesAsync(cancellationToken);
        return asset;
    }

    public async Task<Asset?> UpdateAssetAsync(Asset asset, CancellationToken cancellationToken)
    {
        _tradeDbContext.Assets.Update(asset);
        await _tradeDbContext.SaveChangesAsync(cancellationToken);
        return asset;
    }
}