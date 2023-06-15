using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetById;

public class GetOrderByIdQuery : IRequest<Order>
{
    public GetOrderByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}