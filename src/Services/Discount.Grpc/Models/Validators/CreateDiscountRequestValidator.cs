using System;
using FluentValidation;

namespace Discount.Grpc.Models.Validators;

public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
{
    public CreateDiscountRequestValidator()
    {
        RuleFor(e => e.Coupon.Id).InclusiveBetween(0, int.MaxValue);
        RuleFor(e => e.Coupon).SetValidator(new CouponValidator());
    }
}