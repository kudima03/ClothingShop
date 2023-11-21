using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommandHandler(IRepository<Review> repository) : IRequestHandler<CreateReviewCommand, Review>
{
    public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        Review newReview = new Review
        {
            Comment = request.Comment,
            DateTime = DateTime.Now,
            ProductId = request.ProductId,
            Rate = request.Rate,
            UserId = request.UserId
        };

        try
        {
            Review? review = await repository.InsertAsync(newReview, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return review;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Review)} operation. Check input.");
        }
    }
}