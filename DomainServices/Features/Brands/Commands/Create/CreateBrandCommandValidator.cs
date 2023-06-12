using FluentValidation;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty.");
    }
}