using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using DomainServices.Services.OrdersService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Services.OrdersService;

public class OrdersService : IOrdersService
{
    private readonly IMediator _mediator;
    private readonly IRepository<Order> _ordersRepository;
    private readonly IRepository<OrderStatus> _orderStatusesRepository;
    private readonly IRepository<ProductOption> _productOptionsRepository;
    private readonly IRepository<ShoppingCartItem> _shoppingCartItemsRepository;

    public OrdersService(IRepository<ProductOption> productOptionsRepository,
                         IRepository<OrderStatus> orderStatusesRepository,
                         IRepository<Order> ordersRepository,
                         IMediator mediator,
                         IRepository<ShoppingCartItem> shoppingCartItemsRepository)
    {
        _productOptionsRepository = productOptionsRepository;
        _orderStatusesRepository = orderStatusesRepository;
        _ordersRepository = ordersRepository;
        _mediator = mediator;
        _shoppingCartItemsRepository = shoppingCartItemsRepository;
    }

    public async Task<Order> CreateOrder(long userId,
                                         ICollection<long> shoppingCartItemsIds,
                                         CancellationToken cancellationToken = default)
    {
        await ValidateOrderInitiator(userId, cancellationToken);

        List<OrderItem> orderItems =
            await ValidateAndCreateOrderItems(shoppingCartItemsIds, cancellationToken);

        OrderStatus orderStatus = (await _orderStatusesRepository.GetFirstOrDefaultAsync
                                       (predicate: x => x.Name == OrderStatusName.InReview,
                                        cancellationToken: cancellationToken))!;

        Order newOrder = new Order
        {
            UserId = userId,
            OrderedProductsOptionsInfo = orderItems,
            DateTime = DateTime.UtcNow,
            OrderStatus = orderStatus
        };

        try
        {
            Order? insertedOrder = await _ordersRepository.InsertAsync(newOrder, cancellationToken);
            await _ordersRepository.SaveChangesAsync(cancellationToken);
            await DeleteShoppingCartItems(shoppingCartItemsIds, cancellationToken);
            await _productOptionsRepository.SaveChangesAsync(cancellationToken);

            return insertedOrder;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
    }

    public async Task CancelOrder(long orderId, CancellationToken cancellationToken = default)
    {
        Order order = await ValidateAndGetOrder(orderId, cancellationToken);

        foreach (OrderItem? item in order.OrderedProductsOptionsInfo)
        {
            item.ProductOption.Quantity += item.Amount;
        }

        order.OrderStatus =
            await _orderStatusesRepository.GetFirstOrDefaultAsync
                (predicate: x => x.Name == OrderStatusName.Cancelled, cancellationToken: cancellationToken);

        await _ordersRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateOrderInitiator(long userId, CancellationToken cancellationToken = default)
    {
        bool userExists = await _mediator.Send(new CheckUserExistsQuery(userId), cancellationToken);

        if (!userExists)
        {
            throw new EntityNotFoundException($"User with id:{userId} doesn't exist.");
        }
    }

    private async Task<List<OrderItem>> ValidateAndCreateOrderItems(ICollection<long> shoppingCartItemsIds,
                                                                    CancellationToken cancellationToken = default)
    {
        IList<ShoppingCartItem>? shoppingCartItems =
            await _shoppingCartItemsRepository.GetAllAsync
                (predicate: x => shoppingCartItemsIds.Contains(x.Id), cancellationToken: cancellationToken);

        if (shoppingCartItems.Count != shoppingCartItemsIds.Count)
        {
            IEnumerable<long> missingShoppingCartItems =
                shoppingCartItemsIds.Except(shoppingCartItems.Select(x => x.Id));

            string missingShoppingCartItemsMessage = string.Join(',', missingShoppingCartItems);

            throw new EntityNotFoundException($"ShoppingCartItems with ids:{missingShoppingCartItemsMessage} doesn't exist.");
        }

        return shoppingCartItems.Select
                                    (x => new OrderItem
                                    {
                                        ProductOptionId = x.ProductOptionId,
                                        Amount = x.Amount
                                    })
                                .ToList();
    }

    private async Task DeleteShoppingCartItems(IEnumerable<long> shoppingCartItemsIds,
                                               CancellationToken cancellationToken = default)
    {
        IList<ShoppingCartItem>? itemsToRemove =
            await _shoppingCartItemsRepository.GetAllAsync
                (predicate: x => shoppingCartItemsIds.Contains(x.Id),
                 cancellationToken: cancellationToken);

        _shoppingCartItemsRepository.Delete(itemsToRemove);
    }

    private async Task<Order> ValidateAndGetOrder(long orderId, CancellationToken cancellationToken = default)
    {
        Order? order = await _ordersRepository.GetFirstOrDefaultAsync
                           (x => x.Id == orderId,
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