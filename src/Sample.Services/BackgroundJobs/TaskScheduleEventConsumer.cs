using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Domain.BackgroundJobs;
using Sample.Core.Events;
using Sample.Services.Events;
using Serilog;

namespace Sample.Services.BackgroundJobs
{
    public partial class TaskScheduleEventConsumer :

        IConsumer<EntityInsertedEvent<TaskSchedule>>,
        IConsumer<EntityUpdatedEvent<TaskSchedule>>,
        IConsumer<EntityDeletedEvent<TaskSchedule>>
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);

        #endregion Fields

        #region Constructor

        public TaskScheduleEventConsumer(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public void HandleEvent(EntityInsertedEvent<TaskSchedule> eventMessage)
        {
            Log.Logger.Information("Entity Inserted");
        }

        public void HandleEvent(EntityDeletedEvent<TaskSchedule> eventMessage)
        {
        }

        public void HandleEvent(EntityUpdatedEvent<TaskSchedule> eventMessage)
        {
            Log.Logger.Information("Entity Updated");
        }

        #endregion Methods
    }
}