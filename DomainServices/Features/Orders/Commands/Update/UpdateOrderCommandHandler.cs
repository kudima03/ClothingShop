using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Order;
using DomainServices.Features.Orders.Commands.Create;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IRepository<Order> _ordersRepository;
    private readonly IRepository<ProductOption> _productOptionsRepository;

    public UpdateOrderCommandHandler(IRepository<Order> ordersRepository,
        IRepository<ProductOption> productOptionsRepository)
    {
        _ordersRepository = ordersRepository;
        _productOptionsRepository = productOptionsRepository;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await ValidateAndGetOrderToUpdateAsync(request.OrderId, cancellationToken);

        UpdateExistingOrderedProductOptions(order, request.ProductOptionsIdsAndQuantity);

        List<OrderedProductOption> existingProductOptions =
            await ValidateAndGetOrderedProductOptionsAsync(request.ProductOptionsIdsAndQuantity, cancellationToken);

        List<OrderedProductOption> productOptionsToAdd =
            existingProductOptions.Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparer())
                .ToList();

        DecrementQuantityInRepository(productOptionsToAdd);

        List<OrderedProductOption> productOptionsToRemove =
            order.OrderedProductsOptionsInfo
                .Except(order.OrderedProductsOptionsInfo, new OrderedProductOptionEqualityComparer()).ToList();

        IncrementQuantityInRepository(productOptionsToRemove);

        order.OrderedProductsOptionsInfo.RemoveAll(productOption => productOptionsToRemove.Contains(productOption));

        order.OrderedProductsOptionsInfo.AddRange(productOptionsToAdd);

        try
        {
            await _ordersRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
    }

    private void UpdateExistingOrderedProductOptions(Order order,
        ProductOptionIdAndQuantity[] productOptionsIdsAndQuantity)
    {
        foreach (OrderedProductOption? item in order.OrderedProductsOptionsInfo)
        {
            ProductOptionIdAndQuantity updatedValue =
                productOptionsIdsAndQuantity.Single(x => x.ProductOptionId == item.ProductOptionId);
            item.Amount = updatedValue.Quantity;
        }
    }

    private void DecrementQuantityInRepository(List<OrderedProductOption> orderedProductOptions)
    {
        foreach (OrderedProductOption item in orderedProductOptions)
        {
            item.ProductOption.Quantity -= item.Amount;
        }
    }

    private void IncrementQuantityInRepository(List<OrderedProductOption> abandonedProductOptions)
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

    private async Task<List<OrderedProductOption>> ValidateAndGetOrderedProductOptionsAsync(
        ProductOptionIdAndQuantity[] productsOptionsIdAndQuantity, CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    productsOptionsIdAndQuantity.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != productsOptionsIdAndQuantity.Length)
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

    private class OrderedProductOptionEqualityComparer : IEqualityComparer<OrderedProductOption>
    {
        public bool Equals(OrderedProductOption? x, OrderedProductOption? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.ProductOptionId == y.ProductOptionId;
        }

        public int GetHashCode(OrderedProductOption obj)
        {
            return obj.ProductOptionId.GetHashCode();
        }
    }
}