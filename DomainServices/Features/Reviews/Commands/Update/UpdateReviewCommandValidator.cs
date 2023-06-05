using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
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
            .WithMessage(x =>
                $"{nameof(x.Review.Rate)} must be equal or greater than 0.");
    }
}