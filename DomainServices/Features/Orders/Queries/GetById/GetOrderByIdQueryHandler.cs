using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler(IReadOnlyRepository<Order> ordersRepository) : IRequestHandler<GetOrderByIdQuery, Order>
{
    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order? order = await ordersRepository
                             .ApplySpecification(new OrderWithStatusAndOrderedProductOptions(order => order.Id == request.Id))
                             .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"{nameof(Order)} with id:{request.Id} doesn't exist.");
        }

        return order;
    }
}