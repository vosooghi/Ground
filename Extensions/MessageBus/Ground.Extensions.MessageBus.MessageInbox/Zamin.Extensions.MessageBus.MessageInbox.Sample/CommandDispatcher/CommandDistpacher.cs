using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.RequestResponse.Commands;

namespace Ground.Extensions.MessageBus.MessageInbox.Sample.CommandDispatcher
{
    public class CommandDistpacher : ICommandDispatcher
    {
        Task<CommandResult> ICommandDispatcher.Send<TCommand>(TCommand command)
        {
            throw new NotImplementedException();
        }

        Task<CommandResult<TData>> ICommandDispatcher.Send<TCommand, TData>(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
