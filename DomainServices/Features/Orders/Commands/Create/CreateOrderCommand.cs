using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<Order>
{
    public long UserId { get; init; }

    public ICollection<OrderItemDto> OrderItemsDtos { get; init; }
}