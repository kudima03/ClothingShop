using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetAll;

public class GetAllOrdersQueryHandler(IReadOnlyRepository<Order> ordersRepository) : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await ordersRepository.GetAllAsync(cancellationToken);
    }
}