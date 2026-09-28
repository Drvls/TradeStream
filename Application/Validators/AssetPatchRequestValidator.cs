using FluentValidation;
using TradeStream.Application.DTOs.Request;

namespace TradeStream.Application.Validators;

public class AssetPatchRequestValidator : AbstractValidator<AssetPatchRequest>
{
    public AssetPatchRequestValidator()
    {
        RuleFor(x => x.Code)
            .MaximumLength(6).WithMessage("Asset Code must not exceed 6 characters")
            .When(x => x.Code != null);

        RuleFor(x => x.Name)
            .MaximumLength(50).WithMessage("Asset Name must not exceed 50 characters")
            .When(x => x.Name != null);
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Asset price must be greater than zero")
            .When(x => x.Price != null);
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Asset quantity must be greater than zero")
            .When(x => x.Quantity != null);
    }
}