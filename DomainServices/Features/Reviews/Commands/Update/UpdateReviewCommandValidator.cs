using FluentValidation;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x =>
                $"{nameof(x.Rate)} must be equal or greater than 0.");
    }
}