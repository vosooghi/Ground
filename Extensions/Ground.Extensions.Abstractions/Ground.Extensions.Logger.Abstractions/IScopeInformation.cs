namespace Ground.Extensions.Logger.Abstractions
{
    /// <summary>
    ///  Defines a contract for providing scope information for logging purposes.
    /// </summary>
    public interface IScopeInformation
    {
        /// <summary>
        /// Dictionary containing host-level scope information.
        /// </summary>
        Dictionary<string, object> HostScopeInfo { get; }
        /// <summary>
        /// Dictionary containing request-level scope information.
        /// </summary>
        Dictionary<string, object> RequestScopeInfo { get; }
    }
}
