using TradeStream.Application.DTOs.Response;

namespace TradeStream.Domain.Interfaces;

public interface IAssetService
{
    Task<AssetResponse> GetAssetByCodeAsync(string code, CancellationToken cancellationToken);
}