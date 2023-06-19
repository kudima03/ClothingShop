using FluentValidation;
using System.Text.RegularExpressions;

namespace Infrastructure.Identity.Features.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            //Common regex for phone number
            .Matches(new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$"));

        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword);

        RuleFor(x => x.Password)
            .EmailAddress()
            .NotEmpty()
            //Minimum 8 characters,at least one number,
            //at least one upper case, at least one lower case,
            //at least one special character
            .Matches(new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$"));
    }
}
