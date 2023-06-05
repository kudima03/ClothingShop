using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly IReadOnlyRepository<Order> _ordersRepository;

    public GetOrderByIdQueryHandler(IReadOnlyRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _ordersRepository.ApplySpecification(request.Specification).FirstOrDefaultAsync(cancellationToken);
    }
}