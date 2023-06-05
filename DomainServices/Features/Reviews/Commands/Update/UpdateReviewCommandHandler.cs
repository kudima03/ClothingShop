using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
{
    private readonly IRepository<Review> _repository;

    public UpdateReviewCommandHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        Review? review = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Review.Id,
            cancellationToken: cancellationToken);

        if (review is null)
        {
            throw new EntityNotFoundException($"{nameof(Review)} with id:{request.Review.Id} doesn't exist.");
        }

        review.ProductId = request.Review.ProductId;
        review.Comment = request.Review.Comment;
        review.DateTime = request.Review.DateTime;
        review.Rate = request.Review.Rate;
        review.UserId = request.Review.UserId;

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Review)} operation. Check input.");
        }

        return Unit.Value;
    }
}