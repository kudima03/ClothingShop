using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommandHandler(IRepository<Review> repository) : IRequestHandler<UpdateReviewCommand, Unit>
{
    public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        Review review = await ValidateAndGetReviewAsync(request.Id, cancellationToken);

        review.Comment = request.Comment;
        review.Rate = request.Rate;

        try
        {
            await repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Review)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<Review> ValidateAndGetReviewAsync(long reviewId, CancellationToken cancellationToken = default)
    {
        Review? review = await repository.GetFirstOrDefaultAsync
                             (predicate: x => x.Id == reviewId,
                              cancellationToken: cancellationToken);

        if (review is null)
        {
            throw new EntityNotFoundException($"{nameof(Review)} with id:{reviewId} doesn't exist.");
        }

        return review;
    }
}