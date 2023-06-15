using MediatR;

namespace DomainServices.Features.Reviews.Commands.Update;

public class UpdateReviewCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public int Rate { get; init; }
    public string? Comment { get; init; }
}