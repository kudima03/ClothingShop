using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<Unit>
{
    public long OrderId { get; init; }

    public ICollection<ProductOptionIdAndQuantity> ProductOptionsIdsAndQuantity { get; init; }
}