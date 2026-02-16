namespace Ground.Extensions.ChangeDataLog.Abstractions
{
    /// <summary>
    /// Defines methods for persisting a collection of entity change interceptor items.
    /// </summary>
    public interface IEntityChangeInterceptorItemRepository
    {
        public void Save(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
        public Task SaveAsync(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
    }
}
