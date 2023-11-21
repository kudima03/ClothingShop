using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Cancel;

public class CancelOrderCommandHandler(IOrdersService ordersService) : IRequestHandler<CancelOrderCommand, Unit>
{
    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        await ordersService.CancelOrder(request.Id, cancellationToken);

        return Unit.Value;
    }
}