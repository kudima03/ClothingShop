using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetByUserId;

public class GetOrdersByUserIdQuery(long userId) : IRequest<IEnumerable<Order>>
{
    public long UserId { get; init; } = userId;
}