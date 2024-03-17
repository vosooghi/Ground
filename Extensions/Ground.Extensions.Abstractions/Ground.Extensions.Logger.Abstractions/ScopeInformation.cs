
using System.Reflection;

namespace Ground.Extensions.Logger.Abstractions
{
    public class ScopeInformation : IScopeInformation
    {
        public ScopeInformation()
        {
            HostScopeInfo = new Dictionary<string, object>
              {
                {"MachineName",Environment.MachineName},
                {"EntryPoint",Assembly.GetEntryAssembly().GetName().Name }
              };

            RequestScopeInfo = new Dictionary<string, object>
              {
                {"RequestId",Guid.NewGuid().ToString() }
              };
        }

        public Dictionary<string, object> HostScopeInfo { get; }

        public Dictionary<string, object> RequestScopeInfo { get; }
    }
}

