using TradeStream.Application.DTOs.Request;
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
        Asset asset = await _assetRepository.GetAssetByCodeAsync(code, cancellationToken) ?? throw new AssetNotFoundException(code);
        return new AssetResponse(asset);
    }

    public async Task<AssetResponse> CreateAssetAsync(AssetRequest request, CancellationToken cancellationToken)
    {
        Asset asset = new Asset(
            request.Code,
            request.Name,
            request.Price,
            request.Quantity
        );
        
        asset.PriceHistory.Add(request.Price);
        return new AssetResponse(await _assetRepository.AddAssetAsync(asset, cancellationToken));
    }

    public async Task<AssetResponse> UpdateAssetAsync(Guid id, AssetPatchRequest request, CancellationToken cancellationToken)
    {
        Asset asset = await _assetRepository.FindAssetByIdAsync(id, cancellationToken) ?? throw new AssetNotFoundException(id);
        if(request.Code is not null) asset.ChangeCode(request.Code);
        if(request.Name is not null) asset.ChangeName(request.Name);
        if(request.Price is not null) asset.ChangePrice(request.Price.Value);
        if(request.Quantity is not null) asset.ChangeQuantity(request.Quantity.Value);
        
        return new AssetResponse(await _assetRepository.UpdateAssetAsync(asset, cancellationToken));
    }

    public async Task<IEnumerable<AssetResponse>> GetAssetsAsync(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<Asset> assets = await _assetRepository.GetAssetsAsync(page, size, cancellationToken);
        return assets.Select(asset => new AssetResponse(asset));
    }
}