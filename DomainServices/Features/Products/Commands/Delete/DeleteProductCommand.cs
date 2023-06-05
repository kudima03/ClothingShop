using MediatR;

namespace DomainServices.Features.Products.Commands.Delete;

public class DeleteProductCommand : IRequest<Unit>
{
    public DeleteProductCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}