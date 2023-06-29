using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.ProductOptions.Notifications;
using EntityFrameworkCore.Triggered;
using Infrastructure.Data.Autoremove;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Triggers;
public class AfterShoppingCartItemSavedTrigger : IAfterSaveTrigger<ShoppingCartItem>
{
    private readonly IMediator _mediator;

    private readonly IReadOnlyRepository<ProductOption> _productOptionsRepository;

    private readonly ShoppingCartItemsAutoremoveScheduler _scheduler;

    public AfterShoppingCartItemSavedTrigger(ShoppingCartItemsAutoremoveScheduler scheduler, IMediator mediator,
        IReadOnlyRepository<ProductOption> productOptionsRepository)
    {
        _scheduler = scheduler;
        _mediator = mediator;
        _productOptionsRepository = productOptionsRepository;
    }

    public async Task AfterSave(ITriggerContext<ShoppingCartItem> context, CancellationToken cancellationToken)
    {
        ProductOption? relatedProductOption = await _productOptionsRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == context.Entity.ProductOptionId,
            include: x => x.Include(c => c.ReservedProductOptions),
            cancellationToken: cancellationToken);

        if (relatedProductOption is not null)
        {
            ProductOptionReservedQuantityChangedNotification notification = new(relatedProductOption.Id,
                relatedProductOption.ProductId, relatedProductOption.ReservedProductOptions.Sum(x => x.Amount));

            _mediator.Publish(notification);
        }

        switch (context.ChangeType)
        {
            case ChangeType.Added:
                {
                    await _scheduler.AddToSchedule(context.Entity, cancellationToken);
                    break;
                }
            case ChangeType.Deleted:
                {
                    await _scheduler.RemoveFromSchedule(context.Entity.Id, cancellationToken);
                    break;
                }
            default:
                return;
        }
    }
}