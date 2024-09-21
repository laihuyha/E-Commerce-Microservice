using Basket.API.Application.DTO.Command;
using FluentValidation;

namespace Basket.API.Application.Validators;

public class StoreBasketCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x=>x.Cart).NotNull().WithMessage("Cart cannot be null.");
        RuleFor(x=>x.Cart.UserId).NotNull().WithMessage("UserId is required.");
    }
}
