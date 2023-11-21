using ApplicationCore.Entities;
using DomainServices.Features.ShoppingCartItems.Commands.Delete;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure.Data.Autoremove;

public class RemoveTimedOutShoppingCartItemsJob(IMediator mediator, ILogger<RemoveTimedOutShoppingCartItemsJob> logger)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        long itemId = (long)(context.MergedJobDataMap.Get(JobIdentityName.Id) ?? 0L);
        logger.LogInformation($"Deleting timed out {nameof(ShoppingCartItem)} with id:{itemId}");
        await mediator.Send(new DeleteAndReturnToStockShoppingCartItemCommand(itemId));
        logger.LogInformation($"Deleted timed out {nameof(ShoppingCartItem)} with id:{itemId}");
    }
}