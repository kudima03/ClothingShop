using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Customers.Validators;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCommand<CustomerInfo>>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.UserId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.UserId)} must be greater than 0.");
    }
}