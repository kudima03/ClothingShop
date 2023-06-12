using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<Order>
{
    public long UserId { get; init; }

    public ProductOptionIdAndQuantity[] ProductOptionsIdsAndQuantity { get; init; }
}