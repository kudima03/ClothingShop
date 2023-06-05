using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage($"{nameof(Brand)} cannot be null.");

        RuleFor(x => x.Brand)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Brand.Name)} cannot be null or empty.");

        RuleFor(x => x.Brand.Products)
            .Empty()
            .WithMessage(x => $"{nameof(x.Brand.Products)} must be empty. Create products in appropriate endpoint.");
    }
}