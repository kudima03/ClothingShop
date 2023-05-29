using ApplicationCore.Entities;
using ApplicationCore.Specifications.Order;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Orders.Queries;

public class GetOrderByIdQuery : SingleEntityQuery<Order>
{
    public GetOrderByIdQuery(long id)
        : base(new OrderWithStatusAndProductOptions(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}