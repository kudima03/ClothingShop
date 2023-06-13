using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IRepository<Order> _ordersRepository;

    private readonly IRepository<OrderStatus> _orderStatusesRepository;

    private readonly IRepository<ProductOption> _productOptionsRepository;

    private readonly IRepository<User> _usersRepository;

    public CreateOrderCommandHandler(IRepository<Order> ordersRepository,
        IRepository<ProductOption> productOptionsRepository, IRepository<User> usersRepository,
        IRepository<OrderStatus> orderStatusesRepository)
    {
        _ordersRepository = ordersRepository;
        _productOptionsRepository = productOptionsRepository;
        _usersRepository = usersRepository;
        _orderStatusesRepository = orderStatusesRepository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        User user = await ValidateAndGetOrderInitiator(request.UserId, cancellationToken);

        List<OrderedProductOption> productOptionsToOrder =
            await ValidateAndCreateOrderedProductsOptions(request.ProductOptionsIdsAndQuantity, cancellationToken);

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
            DecrementQuantityInRepository(insertedOrder.OrderedProductsOptionsInfo);
            await _ordersRepository.SaveChangesAsync(cancellationToken);
            return insertedOrder;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
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
        ProductOptionIdAndQuantity[] productsOptionsIdAndQuantity,
        CancellationToken cancellationToken = default)
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

    private void DecrementQuantityInRepository(List<OrderedProductOption> orderedProductOptions)
    {
        foreach (OrderedProductOption item in orderedProductOptions)
        {
            item.ProductOption.Quantity -= item.Amount;
        }
    }
}