using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<Unit>
{
    public long OrderId { get; init; }

    public ICollection<OrderItemDto> ProductOptionsIdsAndQuantity { get; init; }
}