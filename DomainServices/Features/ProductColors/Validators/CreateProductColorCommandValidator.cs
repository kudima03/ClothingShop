using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.ProductColors.Validators;

public class CreateProductColorCommandValidator : AbstractValidator<CreateCommand<ProductColor>>
{
    public CreateProductColorCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.ColorId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.ColorId)} must be greater than 0.");

        RuleFor(x => x.Entity.ImagesInfos)
            .Empty()
            .WithMessage(x =>
                $"{nameof(x.Entity.ImagesInfos)} must be empty. Associate in another request.");
    }
}