using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
{
    private readonly IRepository<Order> _ordersRepository;

    private readonly IRepository<OrderStatus> _orderStatusesRepository;

    public CancelOrderCommandHandler(IRepository<Order> ordersRepository,
        IRepository<OrderStatus> orderStatusesRepository)
    {
        _ordersRepository = ordersRepository;
        _orderStatusesRepository = orderStatusesRepository;
    }

    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await ValidateAndGetOrder(request.Id, cancellationToken);

        foreach (OrderedProductOption? item in order.OrderedProductsOptionsInfo)
        {
            item.ProductOption.Quantity += item.Amount;
        }

        order.OrderStatus = await _orderStatusesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Name == OrderStatusName.Cancelled, cancellationToken: cancellationToken);

        await _ordersRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Order> ValidateAndGetOrder(long orderId, CancellationToken cancellationToken = default)
    {
        Order? order = await _ordersRepository.GetFirstOrDefaultAsync(x => x.Id == orderId,
            x => x.Include(c => c.OrderedProductsOptionsInfo)
                .ThenInclude(c => c.ProductOption),
            cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"{nameof(Order)} with id:{orderId} doesn't exists.");
        }

        return order;
    }
}