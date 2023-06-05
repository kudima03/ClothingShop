using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.User.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.User.Email)} cannot be null or empty");

        RuleFor(x => x.User.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.User.Password)} cannot be null or empty");

        RuleFor(x => x.User.UserTypeId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x =>
                $"{nameof(x.User.UserTypeId)} must be greater than 0.");
    }
}