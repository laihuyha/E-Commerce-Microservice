using Catalog.API.Application.Request;
using Catalog.API.Domain.Enums;
using FastEndpoints;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class UpdateAttributeRequestValidator : Validator<UpdateAttributeRequest>
    {
        public UpdateAttributeRequestValidator()
        {
            _ = RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("Missing AttributeId to udpate!");

            _ = RuleFor(e => (AttributeType)e.AttributeType)
                    .NotEmpty()
                    .WithMessage("Your Attribute's type is required")
                    .IsInEnum();

            _ = RuleFor(e => e.Value)
                    .NotEmpty().WithMessage("Value should not be nulled!");
        }
    }
}