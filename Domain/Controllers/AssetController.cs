using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TradeStream.Application.DTOs.Request;
using TradeStream.Application.DTOs.Response;
using TradeStream.Domain.Interfaces;

namespace TradeStream.Domain.Controllers;

[ApiController]
[Route("tradestream/[controller]")]
public class AssetController(
    IAssetService assetService, 
    IValidator<AssetRequest> validator, 
    IValidator<AssetPatchRequest> patchValidator) : ControllerBase
{
    private readonly IAssetService _assetService = assetService;
    private readonly IValidator<AssetRequest> _assetRequestValidator = validator;
    private readonly IValidator<AssetPatchRequest> _patchValidator = patchValidator;

    [HttpPost]
    public async Task<IActionResult> CreateAsset(AssetRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validation = await _assetRequestValidator.ValidateAsync(request, cancellationToken);
        if(!validation.IsValid) return BadRequest(validation.Errors);

        AssetResponse response = await _assetService.CreateAssetAsync(request, cancellationToken);
        return Created($"assets/{response.Asset.AssetId}", response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsset(string code, CancellationToken cancellationToken) {
        AssetResponse response = await _assetService.GetAssetByCodeAsync(code, cancellationToken);
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAssetCode(Guid id, AssetPatchRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validation = await _patchValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid) return BadRequest(validation.Errors);
        
        AssetResponse response = await _assetService.UpdateAssetAsync(id, request, cancellationToken);
        return Ok(response);
    }
}