namespace Ground.Extensions.ChangeDataLog.Abstractions
{
    /// <summary>
    /// Specifies the type of change made to a database record, such as an insertion, update, or deletion.
    /// </summary>
    public enum DatabaseChangeType
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
