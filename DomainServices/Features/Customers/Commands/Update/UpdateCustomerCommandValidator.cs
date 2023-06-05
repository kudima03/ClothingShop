using FluentValidation;

namespace DomainServices.Features.Customers.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerInfo)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.CustomerInfo.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.CustomerInfo.Id)} out of possible range.");
    }
}