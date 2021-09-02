using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Infrastructure;
using Serilog;
using System;
using System.Linq;

namespace Sample.Services.Events
{
    public partial class EventPublisher : IEventPublisher
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public EventPublisher(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public virtual void Publish<TEvent>(TEvent @event)
        {
            try
            {
                //get all event consumers
                var typeFinder = new AppDomainTypeFinder();
                var types = typeFinder.FindClassesOfType(typeof(IConsumer<TEvent>)).ToList();
                var consumers = types.Select(types => (IConsumer<TEvent>)Activator.CreateInstance(types, args: _memoryCache)).ToList();

                foreach (var consumer in consumers)
                {
                    //try to handle published event
                    consumer.HandleEvent(@event);
                }
            }
            catch (Exception e)
            {
                //log error, we put in to nested try-catch to prevent possible cyclic (if some error occurs)
                try
                {
                    Log.Logger.Error(e, e.Message);
                }
                catch { }
            }
        }

        #endregion Methods
    }
}