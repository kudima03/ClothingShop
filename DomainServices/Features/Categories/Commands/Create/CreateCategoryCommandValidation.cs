using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Create;

internal class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty");

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