using Ground.Core.RequestResponse.Commands;

namespace Ground.Core.Contracts.ApplicationServices.Commands
{
    /// <summary>
    /// Defines the contract for dispatching commands to their appropriate handlers. 
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatch a command as Icommand and find appropraite command for executing.
        /// </summary>
        /// <typeparam name="TCommand">command type</typeparam>
        /// <param name="command">command name</param>
        /// <returns></returns>
        Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

        /// <summary>
        /// Dispatch a command as Icommand and find appropraite command for executing.
        /// </summary>
        /// <typeparam name="TCommand">command type</typeparam>
        /// <typeparam name="TData">return type</typeparam>
        /// <param name="command">command name</param>
        /// <returns></returns>
        Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
    }
}


