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

    public async Task<Order> CreateOrder(long userId, ICollection<ProductOptionIdAndQuantity> productOptionsIdsAndQuantity, CancellationToken cancellationToken = default)
    {
        User user = await ValidateAndGetOrderInitiator(userId, cancellationToken);

        List<OrderedProductOption> productOptionsToOrder =
        await ValidateAndCreateOrderedProductsOptions(productOptionsIdsAndQuantity, cancellationToken);
        DecrementQuantityInRepository(productOptionsToOrder);

        OrderStatus orderStatus = (await _orderStatusesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Name == OrderStatusName.InReview,
            cancellationToken: cancellationToken))!;

        Order newOrder = new()
        {
            User = user,
            OrderedProductsOptionsInfo = productOptionsToOrder,
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

    public async Task UpdateOrder(long orderId, ICollection<ProductOptionIdAndQuantity> productOptionsIdsAndQuantity, CancellationToken cancellationToken = default)
    {
        Order order = await ValidateAndGetOrderToUpdateAsync(orderId, cancellationToken);

        UpdateExistingOrderedProductOptions(order, productOptionsIdsAndQuantity);

        List<OrderedProductOption> existingProductOptions =
            await ValidateAndGetOrderedProductOptionsAsync(productOptionsIdsAndQuantity, cancellationToken);

        IEnumerable<OrderedProductOption> productOptionsToAdd =
            existingProductOptions.Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparerByProductOptionId());

        DecrementQuantityInRepository(productOptionsToAdd);

        IEnumerable<OrderedProductOption> productOptionsToRemove =
            order.OrderedProductsOptionsInfo
                .Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparerByProductOptionId());

        IncrementQuantityInRepository(productOptionsToRemove);

        order.OrderedProductsOptionsInfo.RemoveAll(productOption => productOptionsToRemove.Contains(productOption));

        order.OrderedProductsOptionsInfo.AddRange(productOptionsToAdd);

        try
        {
            await _ordersRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
    }

    public async Task CancelOrder(long orderId, CancellationToken cancellationToken = default)
    {
        Order order = await ValidateAndGetOrder(orderId, cancellationToken);

        foreach (OrderedProductOption? item in order.OrderedProductsOptionsInfo)
        {
            item.ProductOption.Quantity += item.Amount;
        }

        order.OrderStatus = await _orderStatusesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Name == OrderStatusName.Cancelled, cancellationToken: cancellationToken);

        await _ordersRepository.SaveChangesAsync(cancellationToken);

    }

    private async Task<User> ValidateAndGetOrderInitiator(long userId, CancellationToken cancellationToken = default)
    {
        User? user = await _usersRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == userId,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException($"{nameof(User)} with id:{userId} doesn't exist.");
        }

        return user;
    }

    private async Task<List<OrderedProductOption>> ValidateAndCreateOrderedProductsOptions(
        ICollection<ProductOptionIdAndQuantity> productsOptionsIdAndQuantity,
        CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    productsOptionsIdAndQuantity.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != productsOptionsIdAndQuantity.Count)
        {
            throw new EntityNotFoundException("One of ProductOptionsIdsAndQuantity doesn't exist.");
        }

        foreach (ProductOption? item in existingProductOptions)
        {
            ProductOptionIdAndQuantity productOptionIdAndQuantity =
                productsOptionsIdAndQuantity.Single(x => x.ProductOptionId == item.Id);
            if (item.Quantity < productOptionIdAndQuantity.Quantity)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Order.ProductOptionsIdsAndQuantity.Quantity",
                        $"Requested quantity({productOptionIdAndQuantity.Quantity}) for ProductOption with id:{productOptionIdAndQuantity.ProductOptionId} is not available.")
                });
            }
        }

        return productsOptionsIdAndQuantity.Select(x => new OrderedProductOption
        {
            ProductOptionId = x.ProductOptionId,
            ProductOption = existingProductOptions.Single(c => c.Id == x.ProductOptionId),
            Amount = x.Quantity
        }).ToList();
    }

    private void DecrementQuantityInRepository(IEnumerable<OrderedProductOption> orderedProductOptions)
    {
        foreach (OrderedProductOption item in orderedProductOptions)
        {
            item.ProductOption.Quantity -= item.Amount;
        }
    }

    private void UpdateExistingOrderedProductOptions(Order order,
        IEnumerable<ProductOptionIdAndQuantity> productOptionsIdsAndQuantity)
    {
        foreach (OrderedProductOption? item in order.OrderedProductsOptionsInfo)
        {
            ProductOptionIdAndQuantity updatedValue =
                productOptionsIdsAndQuantity.Single(x => x.ProductOptionId == item.ProductOptionId);
            item.Amount = updatedValue.Quantity;
        }
    }

    private void IncrementQuantityInRepository(IEnumerable<OrderedProductOption> abandonedProductOptions)
    {
        foreach (OrderedProductOption item in abandonedProductOptions)
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

    private async Task<List<OrderedProductOption>> ValidateAndGetOrderedProductOptionsAsync(
        IEnumerable<ProductOptionIdAndQuantity> productsOptionsIdAndQuantity, CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    productsOptionsIdAndQuantity.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != productsOptionsIdAndQuantity.Count())
        {
            throw new EntityNotFoundException("One of ProductOptionsIdsAndQuantity doesn't exist.");
        }

        foreach (ProductOption? item in existingProductOptions)
        {
            ProductOptionIdAndQuantity productOptionIdAndQuantity =
                productsOptionsIdAndQuantity.Single(x => x.ProductOptionId == item.Id);
            if (item.Quantity < productOptionIdAndQuantity.Quantity)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Order.ProductOptionsIdsAndQuantity.Quantity",
                        $"Requested quantity({productOptionIdAndQuantity.Quantity}) for ProductOption with id:{productOptionIdAndQuantity.ProductOptionId} is not available.")
                });
            }
        }

        return productsOptionsIdAndQuantity.Select(x => new OrderedProductOption
        {
            ProductOptionId = x.ProductOptionId,
            ProductOption = existingProductOptions.Single(c => c.Id == x.ProductOptionId),
            Amount = x.Quantity
        }).ToList();
    }
}
