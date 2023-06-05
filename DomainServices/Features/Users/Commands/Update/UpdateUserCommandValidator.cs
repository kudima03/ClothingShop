using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
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
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.User.UserTypeId)} must be greater than 0.");
    }
}