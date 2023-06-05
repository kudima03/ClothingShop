using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Customers.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerInfo)
            .NotNull()
            .WithMessage("Object cannot be null");
    }
}