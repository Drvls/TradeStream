using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Entities;
using TradeStream.Domain.Exceptions;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Services;

public class AssetService(IAssetRepository assetRepository) : IAssetService
{
    private readonly IAssetRepository _assetRepository = assetRepository;

    public async Task<AssetResponse> GetAssetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        Asset asset = await _assetRepository.FindByCodeAsync(code, cancellationToken) ?? throw new AssetNotFoundException(code);
        return new AssetResponse(asset);
    }
}