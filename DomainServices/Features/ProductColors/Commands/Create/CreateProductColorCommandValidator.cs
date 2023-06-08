using FluentValidation;

namespace DomainServices.Features.ProductColors.Commands.Create;

public class CreateProductColorCommandValidator : AbstractValidator<CreateProductColorCommand>
{
    public CreateProductColorCommandValidator()
    {
        RuleFor(x => x.ProductColor)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleForEach(x => x.ProductColor.ImagesInfos)
            .NotNull()
            .ChildRules(c => c.RuleFor(v => v.Url).NotNull().NotEmpty());
    }
}