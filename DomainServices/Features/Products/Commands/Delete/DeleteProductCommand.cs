using MediatR;

namespace DomainServices.Features.Products.Commands.Delete;

public class DeleteProductCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}