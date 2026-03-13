namespace Ground.Extensions.ChangeDataLog.Abstractions
{
    /// <summary>
    /// Represents a log item for a property change in an entity.
    /// </summary>
    public class PropertyChangeLogItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChangeInterceptorItemId { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
