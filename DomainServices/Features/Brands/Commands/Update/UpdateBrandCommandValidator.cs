using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.Brand)
            .NotNull()
            .WithMessage($"{nameof(Brand)} cannot be null.");

        RuleFor(x => x.Brand.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Brand.Name)} cannot be null or empty.");

        RuleFor(x => x.Brand.Products)
            .Empty()
            .WithMessage(x => $"{nameof(x.Brand.Products)} must be empty. Update products in appropriate endpoint.");
    }
}