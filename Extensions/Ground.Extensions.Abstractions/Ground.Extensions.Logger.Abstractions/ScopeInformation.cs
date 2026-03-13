
using System.Reflection;

namespace Ground.Extensions.Logger.Abstractions
{
    /// <summary>
    /// Provides contextual information about the current host and request scope, including machine and entry point
    /// details, as well as a unique request identifier.
    /// </summary>
    /// <remarks>Use this class to access scope-specific metadata for logging, diagnostics, or tracing
    /// purposes. The host scope information includes static details about the environment, while the request scope
    /// information contains data unique to each request. All properties are read-only and initialized when the instance
    /// is created.</remarks>
    public class ScopeInformation : IScopeInformation
    {
        public Dictionary<string, object> HostScopeInfo { get; }
        public Dictionary<string, object> RequestScopeInfo { get; }

        public ScopeInformation()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            HostScopeInfo = new Dictionary<string, object>
              {
                {"MachineName", Environment.MachineName},
                {"EntryPoint", entryAssembly != null ? entryAssembly.GetName().Name ?? string.Empty : string.Empty }
              };

            RequestScopeInfo = new Dictionary<string, object>
              {
                {"RequestId", Guid.NewGuid().ToString() }
              };
        }
    }
}

