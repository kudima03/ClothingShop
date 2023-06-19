using FluentValidation;
using System.Text.RegularExpressions;

namespace Infrastructure.Identity.Features.SignIn;
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            //Minimum 8 characters,at least one number,
            //at least one upper case, at least one lower case,
            //at least one special character
            .Matches(new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$"));
    }
}
