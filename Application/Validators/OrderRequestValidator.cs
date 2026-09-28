using FluentValidation;
using TradeStream.Application.DTOs.Request;

namespace TradeStream.Application.Validators;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator() {
        RuleFor(x => x.AssetCode)
            .NotEmpty().WithMessage("Asset code is required")
            .MaximumLength(6).WithMessage("Asset Code must not exceed 6 characters")
            .MinimumLength(3).WithMessage("Asset Code must contain at least 3 characters");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserID is required");

        RuleFor(x => x.TargetValue)
            .NotEmpty().WithMessage("TargetValue is required")
            .GreaterThan(0).WithMessage("TargetValue must be greater than 0");
        
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        RuleFor(order => order.OrderType)
            .IsInEnum().WithMessage("OrderType is required");
    }
}