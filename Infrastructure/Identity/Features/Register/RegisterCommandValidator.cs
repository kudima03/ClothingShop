using ApplicationCore.Constants;
using FluentValidation;

namespace Infrastructure.Identity.Features.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .Matches(RegularExpressions.PhoneNumber);

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters")
            .Matches(RegularExpressions.AtLeastOneDigit)
            .WithMessage("Password must contain at least 1 digit")
            .Matches(RegularExpressions.AtLeastOneLetter)
            .WithMessage("Password must contain at least 1 letter")
            .Matches(RegularExpressions.AtLeastOneLowercase)
            .WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(RegularExpressions.AtLeastOneUppercase)
            .WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(RegularExpressions.AtLeastOneSpecialCharacter)
            .WithMessage("Password must contain at least 1 special character");
    }
}
