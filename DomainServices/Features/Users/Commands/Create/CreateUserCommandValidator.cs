using FluentValidation;

namespace DomainServices.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Email)} cannot be null or empty");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Password)} cannot be null or empty");

        RuleFor(x => x.UserTypeId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x =>
                             $"{nameof(x.UserTypeId)} must be greater than 0.");
    }
}