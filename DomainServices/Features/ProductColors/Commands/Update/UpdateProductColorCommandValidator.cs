using FluentValidation;

namespace DomainServices.Features.ProductColors.Commands.Update;

public class UpdateProductColorCommandValidator : AbstractValidator<UpdateProductColorCommand>
{
    public UpdateProductColorCommandValidator()
    {
        RuleFor(x => x.ProductColor)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.ProductColor.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.ProductColor.Id)} out of possible range.");

        RuleForEach(x => x.ProductColor.ImagesInfos)
            .NotNull()
            .ChildRules(c => c.RuleFor(v => v.Url).NotNull().NotEmpty());
    }
}