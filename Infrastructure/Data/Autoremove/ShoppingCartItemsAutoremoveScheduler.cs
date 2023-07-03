using ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace Infrastructure.Data.Autoremove;

public class ShoppingCartItemsAutoremoveScheduler
{
    private readonly IScheduler _scheduler;

    private readonly TimeSpan _shoppingCartItemLifetime;

    public ShoppingCartItemsAutoremoveScheduler(ISchedulerFactory schedulerFactory, IConfiguration configuration)
    {
        _scheduler = schedulerFactory.GetScheduler().Result;

        _shoppingCartItemLifetime =
            TimeSpan.FromMinutes(configuration.GetValue("ShoppingCartItemLifetimeMinutes", 48 * 60));

        _scheduler.Start();
    }

    private (IJobDetail job, ITrigger trigger) CreateJobForItem(ShoppingCartItem item)
    {
        JobDataMap jobData = new JobDataMap
        {
            new KeyValuePair<string, object>(JobIdentityName.Id, item.Id)
        };

        IJobDetail job = JobBuilder
                         .Create<RemoveTimedOutShoppingCartItemsJob>()
                         .DisallowConcurrentExecution()
                         .SetJobData(jobData)
                         .WithIdentity(new JobKey(item.Id.ToString()))
                         .Build();

        ITrigger trigger = TriggerBuilder.Create()
                                         .WithIdentity(new TriggerKey(item.Id.ToString()))
                                         .StartAt(item.CreationDateTime.Add(_shoppingCartItemLifetime))
                                         .Build();

        return (job, trigger);
    }

    public async Task AddToSchedule(ShoppingCartItem entry, CancellationToken cancellationToken = default)
    {
        (IJobDetail job, ITrigger trigger) = CreateJobForItem(entry);
        await _scheduler.ScheduleJob(job, trigger, cancellationToken);
    }

    public async Task RemoveFromSchedule(long id, CancellationToken cancellationToken = default)
    {
        await _scheduler.DeleteJob(new JobKey(id.ToString()), cancellationToken);
    }
}