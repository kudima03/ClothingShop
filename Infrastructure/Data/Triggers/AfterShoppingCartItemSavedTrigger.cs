using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.ProductOptions.Notifications;
using EntityFrameworkCore.Triggered;
using Infrastructure.Data.Autoremove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Triggers;

public class AfterShoppingCartItemSavedTrigger(ShoppingCartItemsAutoremoveScheduler scheduler,
                                               IMediator mediator,
                                               IReadOnlyRepository<ProductOption> productOptionsRepository)
    : IAfterSaveTrigger<ShoppingCartItem>
{
    public async Task AfterSave(ITriggerContext<ShoppingCartItem> context, CancellationToken cancellationToken)
    {
        ProductOption? relatedProductOption = await productOptionsRepository.GetFirstOrDefaultAsync
                                                  (x => x.Id == context.Entity.ProductOptionId,
                                                   x => x.Include(c => c.ReservedProductOptions),
                                                   cancellationToken);

        if (relatedProductOption is not null)
        {
            ProductOptionReservedQuantityChangedNotification notification = new ProductOptionReservedQuantityChangedNotification
                (relatedProductOption.Id,
                 relatedProductOption.ProductId,
                 relatedProductOption.ReservedProductOptions.Sum(x => x.Amount));

            mediator.Publish(notification);
        }

        switch (context.ChangeType)
        {
            case ChangeType.Added:
                {
                    await scheduler.AddToSchedule(context.Entity, cancellationToken);

                    break;
                }
            case ChangeType.Deleted:
                {
                    await scheduler.RemoveFromSchedule(context.Entity.Id, cancellationToken);

                    break;
                }
            default:
                return;
        }
    }
}