using FluentValidation;

namespace DomainServices.Features.Customers.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}