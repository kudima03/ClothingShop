using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IRepository<Order> _ordersRepository;

    public UpdateOrderCommandHandler(IRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _ordersRepository
            .ApplySpecification(new OrderWithStatusAndProductOptions(x => x.Id == request.Order.Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"{nameof(Order)} with id:{request.Order.Id} doesn't exist.");
        }

        order.UserId = request.Order.UserId;
        order.OrderStatusId = request.Order.OrderStatusId;
        order.DateTime = request.Order.DateTime;
        order.ProductsOptions.Clear();
        order.ProductsOptions.AddRange(request.Order.ProductsOptions);

        try
        {
            await _ordersRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }

        return Unit.Value;
    }
}