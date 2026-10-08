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
        AssetResponse response = new AssetResponse(
            asset.AssetId,
            asset.Code, 
            asset.Name, 
            asset.Price, 
            asset.PriceHistory,
            asset.Quantity,
            asset.IsActive
            );
        return response;
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
        Asset newAsset = await _assetRepository.AddAssetAsync(asset, cancellationToken);
        
        AssetResponse response = new AssetResponse(
            newAsset.AssetId,
            newAsset.Code, 
            newAsset.Name, 
            newAsset.Price, 
            newAsset.PriceHistory,
            newAsset.Quantity,
            newAsset.IsActive
        );
        return response;
    }

    public async Task<AssetResponse> UpdateAssetAsync(Guid id, AssetPatchRequest request, CancellationToken cancellationToken)
    {
        Asset asset = await _assetRepository.FindAssetByIdAsync(id, cancellationToken) ?? throw new AssetNotFoundException(id);
        if(!asset.IsActive) throw new AssetDisabledException(id, asset.Code);
        
        if(request.Code is not null) asset.ChangeCode(request.Code);
        if(request.Name is not null) asset.ChangeName(request.Name);
        if(request.Price is not null) asset.ChangePrice(request.Price.Value);
        if(request.Quantity is not null) asset.ChangeQuantity(request.Quantity.Value);
        
        Asset newAsset = await _assetRepository.UpdateAssetAsync(asset, cancellationToken);
        AssetResponse response = new AssetResponse(
            newAsset.AssetId,
            newAsset.Code, 
            newAsset.Name, 
            newAsset.Price, 
            newAsset.PriceHistory,
            newAsset.Quantity,
            newAsset.IsActive
        );
        return response;
    }

    public async Task<IEnumerable<AssetResponse>> GetAssetsAsync(int page, int size, CancellationToken cancellationToken)
    {
        IEnumerable<Asset> assets = await _assetRepository.GetAssetsAsync(page, size, cancellationToken);
        return assets.Select(asset => new AssetResponse(
            asset.AssetId,
            asset.Code, 
            asset.Name, 
            asset.Price, 
            asset.PriceHistory,
            asset.Quantity,
            asset.IsActive
            ));
    }

    public async Task DisableAssetAsync(Guid assetId, CancellationToken cancellationToken) {
        Asset asset = await _assetRepository.FindAssetByIdAsync(assetId, cancellationToken) ?? throw new AssetNotFoundException(assetId);
        if (!asset.IsActive) throw new AssetAlreadyDisableException(assetId, asset.Code);
        
        asset.IsActive = false;
        await _assetRepository.UpdateAssetActivityStatusAsync(asset, cancellationToken);
    }

    public async Task EnableAssetAsync(Guid assetId, CancellationToken cancellationToken) {
        Asset asset = await _assetRepository.FindAssetByIdAsync(assetId, cancellationToken) ?? throw new AssetNotFoundException(assetId);
        if (asset.IsActive) throw new AssetAlreadyEnableException(assetId, asset.Code);
        
        asset.IsActive = true;
        await _assetRepository.UpdateAssetActivityStatusAsync(asset, cancellationToken);
    }
}