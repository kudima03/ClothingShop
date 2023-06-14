using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
{
    private readonly IOrdersService _ordersService;

    public CancelOrderCommandHandler(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }


    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        await _ordersService.CancelOrder(request.Id, cancellationToken);
        return Unit.Value;
    }
}