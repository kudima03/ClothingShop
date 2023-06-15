using MediatR;

namespace DomainServices.Features.Reviews.Commands.Delete;

public class DeleteReviewCommand : IRequest<Unit>
{
    public DeleteReviewCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}