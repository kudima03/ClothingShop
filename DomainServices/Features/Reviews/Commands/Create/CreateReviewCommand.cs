using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommand : IRequest<Review>
{
    public CreateReviewCommand(Review review)
    {
        Review = review;
    }

    public Review Review { get; init; }
}