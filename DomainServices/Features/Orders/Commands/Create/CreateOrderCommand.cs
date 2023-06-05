using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<Order>
{
    public CreateOrderCommand(Order order)
    {
        Order = order;
    }

    public Order Order { get; init; }
}