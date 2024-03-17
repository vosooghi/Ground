using Ground.Core.RequestResponse.Common;

namespace Ground.Core.RequestResponse.Commands
{
    /// <summary>
    /// the result of each command is returend by this class.
    /// https://github.com/vkhorikov/CqrsInPractice
    /// </summary>
    public class CommandResult : ApplicationServiceResult
    {

    }
    /// <summary>
    /// the result of each command is returend by this class.
    /// https://github.com/vkhorikov/CqrsInPractice
    /// </summary>
    /// <typeparam name="TData">Return Type</typeparam>
    public class CommandResult<TData> : CommandResult
    {
        public TData? _data;
        public TData? Data => _data;
    }
}