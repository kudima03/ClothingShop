using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.EqualityComparers;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Order;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Services;
public class OrdersService : IOrdersService
{
    private readonly IRepository<User> _usersRepository;
    private readonly IRepository<ProductOption> _productOptionsRepository;
    private readonly IRepository<OrderStatus> _orderStatusesRepository;
    private readonly IRepository<Order> _ordersRepository;

    public OrdersService(IRepository<User> usersRepository, IRepository<ProductOption> productOptionsRepository, IRepository<OrderStatus> orderStatusesRepository, IRepository<Order> ordersRepository)
    {
        _usersRepository = usersRepository;
        _productOptionsRepository = productOptionsRepository;
        _orderStatusesRepository = orderStatusesRepository;
        _ordersRepository = ordersRepository;
    }

    public async Task<Order> CreateOrder(long userId, ICollection<OrderItemDto> productOptionsIdsAndQuantity, CancellationToken cancellationToken = default)
    {
        await ValidateOrderInitiator(userId, cancellationToken);

        List<OrderItem> orderItems = await ValidateAndCreateOrderedProductsOptions(productOptionsIdsAndQuantity, cancellationToken);

        OrderStatus orderStatus = (await _orderStatusesRepository.GetFirstOrDefaultAsync(
                                    predicate: x => x.Name == OrderStatusName.InReview,
                                    cancellationToken: cancellationToken))!;

        Order newOrder = new()
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
            DecrementQuantityInRepository(insertedOrder.OrderedProductsOptionsInfo);
            await _productOptionsRepository.SaveChangesAsync(cancellationToken);
            return insertedOrder;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
    }

    public async Task UpdateOrder(long orderId, ICollection<OrderItemDto> orderItemsDtos, CancellationToken cancellationToken = default)
    {
        Order order = await ValidateAndGetOrderToUpdateAsync(orderId, cancellationToken);

        UpdateExistingOrderedProductOptions(order, orderItemsDtos);

        List<OrderItem> existingProductOptions =
            await ValidateAndGetOrderedProductOptionsAsync(orderItemsDtos, cancellationToken);

        IEnumerable<OrderItem> productOptionsToAdd =
            existingProductOptions.Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparerByProductOptionId());

        IEnumerable<OrderItem> productOptionsToRemove =
            order.OrderedProductsOptionsInfo
                .Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparerByProductOptionId());

        order.OrderedProductsOptionsInfo.RemoveAll(productOption => productOptionsToRemove.Contains(productOption));

        order.OrderedProductsOptionsInfo.AddRange(productOptionsToAdd);

        try
        {
            await _ordersRepository.SaveChangesAsync(cancellationToken);
            DecrementQuantityInRepository(productOptionsToAdd);
            IncrementQuantityInRepository(productOptionsToRemove);
            await _productOptionsRepository.SaveChangesAsync(cancellationToken);
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

        order.OrderStatus = await _orderStatusesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Name == OrderStatusName.Cancelled, cancellationToken: cancellationToken);

        await _ordersRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateOrderInitiator(long userId, CancellationToken cancellationToken = default)
    {
        bool userExists = await _usersRepository.ExistsAsync(x => x.Id == userId, cancellationToken);

        if (userExists)
        {
            throw new EntityNotFoundException($"{nameof(User)} with id:{userId} doesn't exist.");
        }
    }

    private async Task<List<OrderItem>> ValidateAndCreateOrderedProductsOptions(
        ICollection<OrderItemDto> orderItemsDtos,
        CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    orderItemsDtos.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != orderItemsDtos.Count)
        {
            IEnumerable<long> missingProductOptions = orderItemsDtos.Select(x=>x.ProductOptionId).Except(existingProductOptions.Select(x => x.Id));
            string missingProductOptionsMessage = string.Join(',', missingProductOptions);
            throw new EntityNotFoundException($"ProductOptionsDtos with ids:{missingProductOptionsMessage} doesn't exist.");
        }

        foreach (ProductOption? item in existingProductOptions)
        {
            OrderItemDto orderItemDto =
                orderItemsDtos.Single(x => x.ProductOptionId == item.Id);
            if (item.Quantity < orderItemDto.Quantity)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Order.OrderItemsDtos.Quantity",
                        $"Requested quantity({orderItemDto.Quantity}) for ProductOption with id:{orderItemDto.ProductOptionId} is not available.")
                });
            }
        }

        return orderItemsDtos.Select(x => new OrderItem
        {
            ProductOptionId = x.ProductOptionId,
            ProductOption = existingProductOptions.Single(c => c.Id == x.ProductOptionId),
            Amount = x.Quantity
        }).ToList();
    }

    private void DecrementQuantityInRepository(IEnumerable<OrderItem> orderedProductOptions)
    {
        foreach (OrderItem item in orderedProductOptions)
        {
            item.ProductOption.Quantity -= item.Amount;
        }
    }

    private void UpdateExistingOrderedProductOptions(Order order,
        IEnumerable<OrderItemDto> productOptionsIdsAndQuantity)
    {
        foreach (OrderItem? item in order.OrderedProductsOptionsInfo)
        {
            OrderItemDto updatedValue =
                productOptionsIdsAndQuantity.Single(x => x.ProductOptionId == item.ProductOptionId);
            item.Amount = updatedValue.Quantity;
        }
    }

    private void IncrementQuantityInRepository(IEnumerable<OrderItem> abandonedProductOptions)
    {
        foreach (OrderItem item in abandonedProductOptions)
        {
            item.ProductOption.Quantity += item.Amount;
        }
    }

    private async Task<Order> ValidateAndGetOrderToUpdateAsync(long orderId,
        CancellationToken cancellationToken = default)
    {
        Order? order = await _ordersRepository
            .ApplySpecification(new OrderWithStatusAndOrderedProductOptions(x => x.Id == orderId))
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"{nameof(Order)} with id:{orderId} doesn't exist.");
        }

        return order;
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

    private async Task<List<OrderItem>> ValidateAndGetOrderedProductOptionsAsync(
        IEnumerable<OrderItemDto> orderItemsDtos, CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    orderItemsDtos.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != orderItemsDtos.Count())
        {
            IEnumerable<long> missingProductOptions = orderItemsDtos.Select(x => x.ProductOptionId).Except(existingProductOptions.Select(x => x.Id));
            string missingProductOptionsMessage = string.Join(',', missingProductOptions);
            throw new EntityNotFoundException($"ProductOptionsDtos with ids:{missingProductOptionsMessage} doesn't exist.");
        }

        foreach (ProductOption? item in existingProductOptions)
        {
            OrderItemDto productOptionIdAndQuantity =
                orderItemsDtos.Single(x => x.ProductOptionId == item.Id);
            if (item.Quantity < productOptionIdAndQuantity.Quantity)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Order.OrderItemsDtos.Quantity",
                        $"Requested quantity({productOptionIdAndQuantity.Quantity}) for ProductOption " +
                        $"with id:{productOptionIdAndQuantity.ProductOptionId} is not available.")
                });
            }
        }

        return orderItemsDtos.Select(x => new OrderItem
        {
            ProductOptionId = x.ProductOptionId,
            ProductOption = existingProductOptions.Single(c => c.Id == x.ProductOptionId),
            Amount = x.Quantity
        }).ToList();
    }
}
