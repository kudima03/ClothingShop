using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Reviews.Commands.Delete;

public class DeleteReviewCommandHandler(IRepository<Review> reviewsRepository) : IRequestHandler<DeleteReviewCommand, Unit>
{
    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        Review? review = await reviewsRepository.GetFirstOrDefaultAsync
                             (predicate: x => x.Id == request.Id,
                              cancellationToken: cancellationToken);

        if (review is not null)
        {
            reviewsRepository.Delete(review);
            await reviewsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}