using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrdersService _ordersService;

    public UpdateOrderCommandHandler(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await _ordersService.UpdateOrder(request.OrderId, request.ProductOptionsIdsAndQuantity, cancellationToken);

        return Unit.Value;
    }
}