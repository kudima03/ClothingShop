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

        IList<ProductOption> existingProductOptions =
            await ValidateAndGetProductOptionsAsync(request.ProductOptionsIdsAndQuantity, cancellationToken);

        IEnumerable<ProductOption> productOptionsToAdd =
            existingProductOptions.Except(order.ProductsOptions, new ProductOptionEqualityComparer());

        IEnumerable<ProductOption> productOptionsToRemove =
            order.ProductsOptions.Except(order.ProductsOptions, new ProductOptionEqualityComparer());

        order.ProductsOptions.RemoveAll(productOption => productOptionsToRemove.Contains(productOption));

        order.ProductsOptions.AddRange(productOptionsToAdd);

        try
        {
            await _ordersRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<Order> ValidateAndGetOrderToUpdateAsync(long orderId,
        CancellationToken cancellationToken = default)
    {
        Order? order = await _ordersRepository
            .ApplySpecification(new OrderWithStatusAndProductOptions(x => x.Id == orderId))
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new EntityNotFoundException($"{nameof(Order)} with id:{orderId} doesn't exist.");
        }

        return order;
    }

    private async Task<IList<ProductOption>> ValidateAndGetProductOptionsAsync(
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

        return existingProductOptions;
    }

    private class ProductOptionEqualityComparer : IEqualityComparer<ProductOption>
    {
        public bool Equals(ProductOption? x, ProductOption? y)
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

            return x.Id == y.Id;
        }

        public int GetHashCode(ProductOption obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}