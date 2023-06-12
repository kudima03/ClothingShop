using DomainServices.Features.Orders.Commands.Create;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<Unit>
{
    public long OrderId { get; init; }

    public ProductOptionIdAndQuantity[] ProductOptionsIdsAndQuantity { get; init; }
}