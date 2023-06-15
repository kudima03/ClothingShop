using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrdersService _ordersService;

    public CreateOrderCommandHandler(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return await _ordersService.CreateOrder(request.UserId, request.OrderItemsDtos,
            cancellationToken);
    }
}