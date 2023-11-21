using MediatR;

namespace DomainServices.Features.Orders.Commands.Cancel;

public class CancelOrderCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}