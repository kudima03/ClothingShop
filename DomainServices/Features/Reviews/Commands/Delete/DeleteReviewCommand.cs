using MediatR;

namespace DomainServices.Features.Reviews.Commands.Delete;

public class DeleteReviewCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}