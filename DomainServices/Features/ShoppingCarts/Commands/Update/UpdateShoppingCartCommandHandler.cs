using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DomainServices.Features.ShoppingCarts.Commands.UpdateShoppingCartDtos;

namespace DomainServices.Features.ShoppingCarts.Commands.Update;

public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, Unit>
{
    private readonly IRepository<ProductOption> _productOptionsRepository;
    private readonly IRepository<ShoppingCart> _shoppingCartRepository;

    public UpdateShoppingCartCommandHandler(IRepository<ShoppingCart> shoppingCartRepository,
                                            IRepository<ProductOption> productOptionsRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productOptionsRepository = productOptionsRepository;
    }

    public async Task<Unit> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCart = await ValidateAndGetShoppingCart(request.UserId, cancellationToken);

        UpdateExistingItems(shoppingCart, request.ItemsDtos);

        ICollection<ShoppingCartItem> itemsToAdd = await ValidateAndGetShoppingCartItemsToAdd(shoppingCart, request.ItemsDtos);

        ICollection<ShoppingCartItem> itemsToRemove = ValidateAndGetShoppingCartItemsToRemove(shoppingCart, request.ItemsDtos);

        shoppingCart.Items.RemoveAll(productOption => itemsToRemove.Contains(productOption));

        shoppingCart.Items.AddRange(itemsToAdd);

        try
        {
            await _shoppingCartRepository.SaveChangesAsync(cancellationToken);
            DecrementQuantityInRepository(itemsToAdd);
            IncrementQuantityInRepository(itemsToRemove);
            await _productOptionsRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(ShoppingCart)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<ICollection<ShoppingCartItem>> ValidateAndGetShoppingCartItemsToAdd(
        ShoppingCart shoppingCart,
        ICollection<ShoppingCartItemDto> requestedItems)
    {
        IEnumerable<ShoppingCartItemDto> newItems = requestedItems.Where
            (x => !shoppingCart.Items.Select(c => c.ProductOptionId).Contains(x.ProductOptionId));

        IList<ProductOption>? productOptions = await _productOptionsRepository.GetAllAsync
                                                   (predicate: x => newItems.Select(c => c.ProductOptionId).Contains(x.Id));

        if (productOptions.Count != newItems.Count())
        {
            IEnumerable<long> missingShoppingCartItems = newItems.Select
                                                                     (x => x.ProductOptionId)
                                                                 .Except(productOptions.Select(x => x.Id));

            string missingShoppingCartItemsMessage = string.Join(',', missingShoppingCartItems);

            throw new EntityNotFoundException($"ShoppingCartItemsDtos with ids:{missingShoppingCartItemsMessage} doesn't exist.");
        }

        foreach (ShoppingCartItemDto item in newItems)
        {
            ProductOption? productOption = productOptions.Single(x => x.Id == item.ProductOptionId);

            if (item.Quantity > productOption.Quantity)
            {
                throw new ValidationException
                    (new[]
                    {
                        new ValidationFailure
                            ("Order.OrderItemsDtos.Quantity",
                             $"Requested quantity({item.Quantity}) for ProductOption " +
                             $"with id:{item.ProductOptionId} is not available.")
                    });
            }
        }

        return newItems.Select
                           (x => new ShoppingCartItem
                           {
                               Amount = x.Quantity,
                               ProductOptionId = x.ProductOptionId
                           })
                       .ToList();
    }

    private void IncrementQuantityInRepository(IEnumerable<ShoppingCartItem> abandonedShoppingCartItems)
    {
        foreach (ShoppingCartItem item in abandonedShoppingCartItems)
        {
            item.ProductOption.Quantity += item.Amount;
        }
    }

    private void DecrementQuantityInRepository(IEnumerable<ShoppingCartItem> orderedShoppingCartItems)
    {
        foreach (ShoppingCartItem item in orderedShoppingCartItems)
        {
            item.ProductOption.Quantity -= item.Amount;
        }
    }

    private ICollection<ShoppingCartItem> ValidateAndGetShoppingCartItemsToRemove(
        ShoppingCart shoppingCart,
        ICollection<ShoppingCartItemDto> requestedItems)
    {
        IEnumerable<ShoppingCartItem> itemsToRemove = shoppingCart.Items.Where
            (x => !requestedItems.Select(c => c.ProductOptionId).Contains(x.ProductOptionId));

        return itemsToRemove.ToList();
    }

    private void UpdateExistingItems(ShoppingCart shoppingCart,
                                     IEnumerable<ShoppingCartItemDto> shoppingCartItemsDtos)
    {
        foreach (ShoppingCartItem? item in shoppingCart.Items)
        {
            ShoppingCartItemDto? updatedValue = shoppingCartItemsDtos.SingleOrDefault
                (x => x.ProductOptionId == item.ProductOptionId);

            if (updatedValue is not null && updatedValue.Quantity != item.Amount)
            {
                int diff = item.Amount - updatedValue.Quantity;

                if (diff < 0 && item.ProductOption.Quantity - Math.Abs(diff) < 0)
                {
                    throw new ValidationException
                        (new[]
                        {
                            new ValidationFailure
                                ("Order.OrderItemsDtos.Quantity",
                                 $"Requested quantity({item.Amount}) for ProductOption " +
                                 $"with id:{item.ProductOptionId} is not available.")
                        });
                }

                item.ProductOption.Quantity += diff;

                item.Amount = updatedValue.Quantity;
            }
        }
    }

    private async Task<ShoppingCart> ValidateAndGetShoppingCart(long userId, CancellationToken cancellationToken = default)
    {
        ShoppingCart? shoppingCart =
            await _shoppingCartRepository.GetFirstOrDefaultAsync
                (x => x.UserId == userId,
                 x => x.Include(c => c.Items).ThenInclude(c => c.ProductOption),
                 cancellationToken);

        if (shoppingCart is null)
        {
            throw new EntityNotFoundException($"{nameof(ShoppingCart)} with id:{shoppingCart} doesn't exist.");
        }

        return shoppingCart;
    }
}