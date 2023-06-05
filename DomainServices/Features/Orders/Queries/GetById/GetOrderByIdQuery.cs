using ApplicationCore.Entities;
using ApplicationCore.Specifications.Order;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetById;

public class GetOrderByIdQuery : IRequest<Order?>
{
    public GetOrderByIdQuery(long id)
    {
        Id = id;
        Specification = new OrderWithStatusAndProductOptions(order => order.Id == id);
    }

    public long Id { get; init; }

    public OrderWithStatusAndProductOptions Specification { get; init; }
}