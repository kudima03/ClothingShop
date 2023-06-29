using ApplicationCore.Entities;
using ApplicationCore.EqualityComparers;
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
    private readonly IRepository<ShoppingCart> _shoppingCartRepository;
    private readonly IRepository<ProductOption> _productOptionsRepository;

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

        ICollection<ShoppingCartItem> existingItems =
            await ValidateAndGetProductOptionsAsync(request.ItemsDtos, cancellationToken);
        
        IEnumerable<ShoppingCartItem> itemsToAdd =
            existingItems.Except(shoppingCart.Items, new ShoppingCartItemEqualityComparerByProductOptionId());

        ValidateItemsToAddQuantity(existingItems, itemsToAdd);

        IEnumerable<ShoppingCartItem> itemsToRemove =
            shoppingCart.Items
                .Except(existingItems, new ShoppingCartItemEqualityComparerByProductOptionId());

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

    private async Task<ICollection<ShoppingCartItem>> ValidateAndGetProductOptionsAsync(
        IEnumerable<UpdateShoppingCartDtos.ShoppingCartItemDto> shoppingCartItemsDtos,
        CancellationToken cancellationToken = default)
    {
        IList<ProductOption>? existingProductOptions =
            await _productOptionsRepository.GetAllAsync(predicate: productOption =>
                    shoppingCartItemsDtos.Select(x => x.ProductOptionId).Contains(productOption.Id),
                cancellationToken: cancellationToken);

        if (existingProductOptions.Count != shoppingCartItemsDtos.Count())
        {
            IEnumerable<long> missingShoppingCartItems = shoppingCartItemsDtos.Select(x => x.ProductOptionId).Except(existingProductOptions.Select(x => x.Id));
            string missingShoppingCartItemsMessage = string.Join(',', missingShoppingCartItems);
            throw new EntityNotFoundException($"ShoppingCartItemsDtos with ids:{missingShoppingCartItemsMessage} doesn't exist.");
        }
        
        return shoppingCartItemsDtos.Select(x => new ShoppingCartItem
        {
            ProductOptionId = x.ProductOptionId,
            ProductOption = existingProductOptions.Single(c => c.Id == x.ProductOptionId),
            Amount = x.Quantity
        }).ToList();
    }

    private void ValidateItemsToAddQuantity(IEnumerable<ShoppingCartItem> existingItems, IEnumerable<ShoppingCartItem> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            var existingItem =
                existingItems.Single(x => x.ProductOptionId == item.ProductOptionId);
            if (existingItem.Amount - item.Amount  < 0)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Order.OrderItemsDtos.Quantity",
                        $"Requested quantity({item.Amount}) for ProductOption " +
                        $"with id:{item.ProductOptionId} is not available.")
                });
            }
        }
    }

    private void UpdateExistingItems(ShoppingCart shoppingCart,
        IEnumerable<UpdateShoppingCartDtos.ShoppingCartItemDto> shoppingCartItemsDtos)
    {
        foreach (ShoppingCartItem? item in shoppingCart.Items)
        {
            UpdateShoppingCartDtos.ShoppingCartItemDto? updatedValue = shoppingCartItemsDtos.SingleOrDefault(x => x.ProductOptionId == item.ProductOptionId);
            if (updatedValue is not null)
            {
                int diff = item.Amount - updatedValue.Quantity;

                if (diff < 0 && item.ProductOption.Quantity - Math.Abs(diff) < 0)
                {
                    throw new ValidationException(new[]
                    {
                        new ValidationFailure("Order.OrderItemsDtos.Quantity",
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
            await _shoppingCartRepository.GetFirstOrDefaultAsync(predicate: x => x.UserId == userId,
                include: x => x.Include(c => c.Items).ThenInclude(c => c.ProductOption),
                cancellationToken: cancellationToken);

        if (shoppingCart is null)
        {
            throw new EntityNotFoundException($"{nameof(ShoppingCart)} with id:{shoppingCart} doesn't exist.");
        }

        return shoppingCart;
    }
}