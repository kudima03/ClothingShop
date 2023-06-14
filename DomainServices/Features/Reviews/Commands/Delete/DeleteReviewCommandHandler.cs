using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Reviews.Commands.Delete;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
{
    private readonly IRepository<Review> _reviewsRepository;

    public DeleteReviewCommandHandler(IRepository<Review> reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }

    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        Review? review = await _reviewsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                                                                         cancellationToken: cancellationToken);

        if (review is not null)
        {
            _reviewsRepository.Delete(review);
            await _reviewsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}