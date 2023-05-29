using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.ImagesInfos.Validators;

public class UpdateImageInfoCommandValidator : AbstractValidator<UpdateCommand<ImageInfo>>
{
    public UpdateImageInfoCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.ProductColorId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.ProductColorId)} must be greater than 0.");
    }
}