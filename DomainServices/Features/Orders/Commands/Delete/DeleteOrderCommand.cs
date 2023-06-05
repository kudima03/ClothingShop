using MediatR;

namespace DomainServices.Features.Orders.Commands.Delete;

public class DeleteOrderCommand : IRequest<Unit>
{
    public DeleteOrderCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}