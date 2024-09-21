using Basket.API.Application.DTO.Command;
using FluentValidation;

namespace Basket.API.Application.Validators;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(e=>e.UserId).NotNull().WithMessage("CartId is required");
    }
}
