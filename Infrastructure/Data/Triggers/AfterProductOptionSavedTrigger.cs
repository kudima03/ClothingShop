using ApplicationCore.Entities;
using DomainServices.Features.ProductOptions.Notifications;
using EntityFrameworkCore.Triggered;
using MediatR;

namespace Infrastructure.Data.Triggers;

public class AfterProductOptionSavedTrigger : IAfterSaveTrigger<ProductOption>
{
    private readonly IMediator _mediator;

    public AfterProductOptionSavedTrigger(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task AfterSave(ITriggerContext<ProductOption> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType != ChangeType.Modified)
        {
            return Task.CompletedTask;
        }

        long productOptionId = context.Entity.Id;
        long productId = context.Entity.ProductId;
        int newQuantity = context.Entity.Quantity;
        int? oldQuantity = context.UnmodifiedEntity?.Quantity;

        if (oldQuantity != newQuantity)
        {
            return _mediator.Publish
                (new ProductOptionQuantityChangedNotification(productOptionId, productId, newQuantity),
                 cancellationToken);
        }

        return Task.CompletedTask;
    }
}