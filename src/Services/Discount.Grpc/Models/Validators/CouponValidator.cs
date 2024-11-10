using FluentValidation;

namespace Discount.Grpc.Models.Validators;

public class CouponValidator : AbstractValidator<Grpc.Coupon>
{
    public CouponValidator()
    {
        RuleFor(e => e.ProductName).NotEmpty();
        RuleFor(e => (DiscountType)e.Type).NotNull().IsInEnum();
        When(e => e.Type is 1,
                () => { RuleFor(e => e.Amount).NotEmpty().InclusiveBetween(0, double.MaxValue); })
            .Otherwise(() => { RuleFor(e => e.Amount).NotEmpty().InclusiveBetween(0, 100); });
    }
}
