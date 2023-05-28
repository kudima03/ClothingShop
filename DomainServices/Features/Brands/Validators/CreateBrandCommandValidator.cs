using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Brands.Validators;

public class CreateBrandCommandValidator : AbstractValidator<CreateCommand<Brand>>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Brand cannot be null.");

        RuleFor(x => x.Entity.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Entity.Id)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage($"Id cannot be greater than {long.MaxValue}.");

        RuleFor(x => x.Entity.Name)
            .NotEmpty()
            .WithMessage("Name cannot be null or empty.");

        RuleFor(x => x.Entity.Products)
            .NotNull()
            .WithMessage("Products cannot be null.");
    }
}