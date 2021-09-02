namespace Sample.Core.Events
{
    public class EntityDeletedEvent<T> where T : BaseEntity
    {
        public EntityDeletedEvent(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}