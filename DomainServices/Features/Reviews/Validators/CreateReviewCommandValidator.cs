using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Reviews.Validators;

public class CreateReviewCommandValidator : AbstractValidator<CreateCommand<Review>>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.ProductId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.ProductId)} must be greater than 0.");

        RuleFor(x => x.Entity.UserId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.UserId)} must be greater than 0.");

        RuleFor(x => x.Entity.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.Rate)} must be equal or greater than 0.");

        RuleFor(x => x.Entity.Comment)
            .NotNull()
            .NotEmpty()
            .WithMessage(x =>
                $"{nameof(x.Entity.Comment)} cannot be null or empty.");
    }
}