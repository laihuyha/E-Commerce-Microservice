using Catalog.API.Application.Request;
using Catalog.API.Domain.Enums;
using FastEndpoints;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class CreateAttributeRequestValidator : Validator<CreateAttributeRequest>
    {
        public CreateAttributeRequestValidator()
        {
            _ = RuleFor(e => (AttributeType)e.AttributeType)
                    .NotEmpty()
                    .WithMessage("Your Attribute's type is required")
                    .IsInEnum();

            _ = RuleFor(e => e.Value)
                    .NotEmpty().WithMessage("Value should not be nulled!");
        }
    }
}