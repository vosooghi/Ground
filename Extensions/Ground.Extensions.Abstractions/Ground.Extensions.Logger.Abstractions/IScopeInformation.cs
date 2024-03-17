namespace Ground.Extensions.Logger.Abstractions
{
    public interface IScopeInformation
    {
        Dictionary<string, object> HostScopeInfo { get; }
        Dictionary<string, object> RequestScopeInfo { get; }
    }
}
