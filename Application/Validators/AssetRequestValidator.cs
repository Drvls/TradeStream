using FluentValidation;
using TradeStream.Application.DTOs.Request;

namespace TradeStream.Application.Validators;

public class AssetRequestValidator : AbstractValidator<AssetRequest>
{
    public AssetRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Asset Code is required")
            .MaximumLength(6).WithMessage("Asset Code must not exceed 6 characters")
            .MinimumLength(3).WithMessage("Asset Code must contain at least 3 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Asset Name is required")
            .MaximumLength(50).WithMessage("Asset Name must not exceed 50 characters")
            .MinimumLength(1).WithMessage("Asset Name must contain at least 1 character");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Asset Price is required")
            .GreaterThan(0).WithMessage("Asset Price must be greater than 0");
        
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Asset Quantity is required")
            .GreaterThan(0).WithMessage("Asset Quantity must be greater than 0");
    }
}