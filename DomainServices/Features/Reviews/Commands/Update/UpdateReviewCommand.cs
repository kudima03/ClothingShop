using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommand : IRequest<Unit>
{
    public UpdateReviewCommand(Review review)
    {
        Review = review;
    }

    public Review Review { get; init; }
}