using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommand : IRequest<Review>
{
    public int Rate { get; init; }
    public string? Comment { get; init; }
    public long UserId { get; init; }
    public long ProductId { get; init; }
}