using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler(IOrdersService ordersService) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return await ordersService.CreateOrder
                   (request.UserId,
                    request.ShoppingCartItemsIds,
                    cancellationToken);
    }
}