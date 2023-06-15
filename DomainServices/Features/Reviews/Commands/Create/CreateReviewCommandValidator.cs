using FluentValidation;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.ProductId)} out of possible range.");

        RuleFor(x => x.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x => $"{nameof(x.Rate)} out of possible range.");
    }
}