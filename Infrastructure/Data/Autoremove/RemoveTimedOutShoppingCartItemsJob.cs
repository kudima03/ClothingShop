using ApplicationCore.Entities;
using DomainServices.Features.ShoppingCartItems.Commands.Delete;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure.Data.Autoremove;
public class RemoveTimedOutShoppingCartItemsJob : IJob
{
    private readonly IMediator _mediator;

    private readonly ILogger<RemoveTimedOutShoppingCartItemsJob> _logger;

    public RemoveTimedOutShoppingCartItemsJob(IMediator mediator, ILogger<RemoveTimedOutShoppingCartItemsJob> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        long itemId = (long)(context.MergedJobDataMap.Get(JobIdentityName.Id) ?? 0L);
        _logger.LogInformation($"Deleting timed out {nameof(ShoppingCartItem)} with id:{itemId}");
        await _mediator.Send(new DeleteAndReturnToStockShoppingCartItemCommand(itemId));
        _logger.LogInformation($"Deleted timed out {nameof(ShoppingCartItem)} with id:{itemId}");
    }
}