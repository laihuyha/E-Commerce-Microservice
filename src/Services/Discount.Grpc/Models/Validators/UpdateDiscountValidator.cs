using FluentValidation;

namespace Discount.Grpc.Models.Validators;

public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountValidator()
    {
        RuleFor(e => e.Coupon.Id).NotEmpty().NotNull().InclusiveBetween(0, int.MaxValue);
        RuleFor(e => e.Coupon).SetValidator(new CouponValidator());
    }
}