using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<Unit>
{
    public UpdateOrderCommand(Order order)
    {
        Order = order;
    }

    public Order Order { get; init; }
}