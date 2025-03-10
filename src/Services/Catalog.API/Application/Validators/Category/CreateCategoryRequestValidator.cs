using Catalog.API.Application.Request;
using FastEndpoints;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class CreateCategoryRequestValidator : Validator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            _ = RuleFor(e => e.Name)
                    .NotEmpty()
                    .WithMessage("Your Category's name is required");

            _ = RuleFor(e => e.Description)
                    .NotEmpty().WithMessage("Category need a description.")
                    .MinimumLength(10).WithMessage("The description must be 10 characters at least.")
                    .MaximumLength(1024).WithMessage("The description too long. Maximum characters are 1024");
        }
    }
}