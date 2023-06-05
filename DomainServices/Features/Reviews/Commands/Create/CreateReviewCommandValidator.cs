using FluentValidation;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Review)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Review.ProductId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Review.ProductId)} out of possible range.");

        RuleFor(x => x.Review.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Review.UserId)} out of possible range.");

        RuleFor(x => x.Review.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x => $"{nameof(x.Review.Rate)} out of possible range.");
    }
}