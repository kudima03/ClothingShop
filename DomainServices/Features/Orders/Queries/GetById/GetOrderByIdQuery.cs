using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetById;

public class GetOrderByIdQuery(long id) : IRequest<Order>
{
    public long Id { get; init; } = id;
}