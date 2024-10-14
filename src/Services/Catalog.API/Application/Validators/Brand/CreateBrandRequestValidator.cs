using Catalog.API.Application.Request;
using FastEndpoints;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class CreateBrandRequestValidator : Validator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            _ = RuleFor(e => e.Name)
                    .NotEmpty()
                    .WithMessage("Your Brand's name is required.");

            _ = RuleFor(e => e.Description)
                    .NotEmpty().WithMessage("Brand need a description.")
                    .MinimumLength(100).WithMessage("The description must be 100 characters at least.")
                    .MaximumLength(4096).WithMessage("The description too long. Maximum characters are 4096");

            _ = RuleFor(x => x.LogoUrl)
                    .MinimumLength(7)
                    .When(x => !string.IsNullOrEmpty(x.Name) && x.LogoUrl.Length > 0).Must(x => x.Length > 0)
                    .WithMessage("The Brand should have a image");
        }
    }
}