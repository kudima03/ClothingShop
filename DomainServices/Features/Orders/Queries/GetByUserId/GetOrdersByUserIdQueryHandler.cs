using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Queries.GetByUserId;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<Order>>
{
    private readonly IReadOnlyRepository<Order> _ordersRepository;

    public GetOrdersByUserIdQueryHandler(IReadOnlyRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<IEnumerable<Order>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _ordersRepository.GetAllAsync
                   (predicate: x => x.UserId == request.UserId,
                    include: x => x.Include(c => c.OrderStatus)
                                   .Include(c => c.OrderedProductsOptionsInfo)
                                   .ThenInclude(v => v.ProductOption.Product)
                                   .Include(c => c.OrderedProductsOptionsInfo)
                                   .ThenInclude(v => v.ProductOption.ProductColor.ImagesInfos),
                    cancellationToken: cancellationToken);
    }
}