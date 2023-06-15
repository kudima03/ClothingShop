using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetAll;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IReadOnlyRepository<Order> _ordersRepository;

    public GetAllOrdersQueryHandler(IReadOnlyRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _ordersRepository.GetAllAsync(cancellationToken);
    }
}