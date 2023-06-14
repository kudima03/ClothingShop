using FluentValidation;

namespace DomainServices.Features.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
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
            .GreaterThan(0)
            .WithMessage(x =>
                             $"{nameof(x.UserTypeId)} must be greater than 0.");
    }
}