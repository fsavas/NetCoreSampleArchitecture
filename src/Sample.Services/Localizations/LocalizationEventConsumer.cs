using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Defaults;
using Sample.Core.Domain.Localizations;
using Sample.Core.Events;
using Sample.Services.Events;

namespace Sample.Services.Localizations
{
    public partial class LocalizationEventConsumer :

        IConsumer<EntityInsertedEvent<Language>>,
        IConsumer<EntityUpdatedEvent<Language>>,
        IConsumer<EntityDeletedEvent<Language>>,

        IConsumer<EntityInsertedEvent<LocaleResource>>,
        IConsumer<EntityUpdatedEvent<LocaleResource>>,
        IConsumer<EntityDeletedEvent<LocaleResource>>
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);

        #endregion Fields

        #region Constructor

        public LocalizationEventConsumer(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        #region Language

        public void HandleEvent(EntityInsertedEvent<Language> eventMessage)
        {
            _memoryCache.Set(MemoryCacheKeys.Language, eventMessage.Entity, cacheOptions);
        }

        public void HandleEvent(EntityDeletedEvent<Language> eventMessage)
        {
            _memoryCache.Remove(MemoryCacheKeys.Language);
        }

        public void HandleEvent(EntityUpdatedEvent<Language> eventMessage)
        {
            _memoryCache.Set(MemoryCacheKeys.Language, eventMessage.Entity, cacheOptions);
        }

        #endregion Language

        #region LocaleResource

        public void HandleEvent(EntityInsertedEvent<LocaleResource> eventMessage)
        {
            _memoryCache.Set(string.Join(MemoryCacheKeys.KeySeperator, eventMessage.Entity.Name, eventMessage.Entity.LanguageId), eventMessage.Entity.Value, cacheOptions);
        }

        public void HandleEvent(EntityDeletedEvent<LocaleResource> eventMessage)
        {
            _memoryCache.Remove(string.Join(MemoryCacheKeys.KeySeperator, eventMessage.Entity.Name, eventMessage.Entity.LanguageId));
        }

        public void HandleEvent(EntityUpdatedEvent<LocaleResource> eventMessage)
        {
            _memoryCache.Set(string.Join(MemoryCacheKeys.KeySeperator, eventMessage.Entity.Name, eventMessage.Entity.LanguageId), eventMessage.Entity.Value, cacheOptions);
        }

        #endregion LocaleResource

        #endregion Methods
    }
}