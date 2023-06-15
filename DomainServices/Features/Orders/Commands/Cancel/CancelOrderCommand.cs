using MediatR;

namespace DomainServices.Features.Orders.Commands.Cancel;

public class CancelOrderCommand : IRequest<Unit>
{
    public CancelOrderCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}