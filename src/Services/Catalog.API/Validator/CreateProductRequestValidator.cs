using Catalog.API.Request.Product;
using FastEndpoints;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class CreateProductRequestValidator : Validator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            _ = RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Your product's name is required");

            _ = RuleFor(e => e.Category).NotEmpty().WithMessage("Please choose the category for product!")
                                        .Must(e => !e.Exists(x => string.IsNullOrEmpty(x)))
                                        .WithMessage("Little cunt! Check category again!");

            _ = RuleFor(x => x.ImageFile)
            .MinimumLength(7)
            .When(x => !string.IsNullOrEmpty(x.Name) && x.ImageFile.Length > 0).Must(x => x.Length > 0)
            .WithMessage("The product should have a image");

            _ = RuleFor(x => x.Price).GreaterThan(0).WithMessage("Value of project should be greater than 0");
        }
    }
}