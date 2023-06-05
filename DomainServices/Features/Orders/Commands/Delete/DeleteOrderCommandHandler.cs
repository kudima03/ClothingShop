using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Delete;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IRepository<Order> _ordersRepository;

    public DeleteOrderCommandHandler(IRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _ordersRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (order is not null)
        {
            _ordersRepository.Delete(order);
            await _ordersRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}