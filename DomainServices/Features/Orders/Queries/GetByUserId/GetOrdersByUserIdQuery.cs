using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetByUserId;
public class GetOrdersByUserIdQuery : IRequest<IEnumerable<Order>>
{
    public GetOrdersByUserIdQuery(long userId)
    {
        UserId = userId;
    }
    public long UserId { get; init; }
}