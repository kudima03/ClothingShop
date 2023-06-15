using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty.");

        RuleFor(x => x.SectionsIds)
            .NotNull();

        RuleForEach(x => x.SectionsIds)
            .NotNull()
            .ChildRules(c =>
            {
                c.RuleFor(id => id).InclusiveBetween(1, long.MaxValue)
                    .WithMessage("Section id out of possible range.");
            });
    }
}