namespace Ground.Extensions.ChangeDataLog.Abstractions
{
    public interface IEntityChageInterceptorItemRepository
    {
        public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems);
        public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems);
    }
}
