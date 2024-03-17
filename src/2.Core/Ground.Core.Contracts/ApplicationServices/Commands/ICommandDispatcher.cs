using Ground.Core.RequestResponse.Commands;

namespace Ground.Core.Contracts.ApplicationServices.Commands
{
    /// <summary>
    /// Mediator pattern to manage commands structure.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// get a command as Icommand and find appropraite command for executing.
        /// </summary>
        /// <typeparam name="TCommand">command type</typeparam>
        /// <param name="command">command name</param>
        /// <returns></returns>
        Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

        /// <summary>
        /// get a command as Icommand and find appropraite command for executing. it has return value.
        /// </summary>
        /// <typeparam name="TCommand">command type</typeparam>
        /// <typeparam name="TData">return type</typeparam>
        /// <param name="command">command name</param>
        /// <returns></returns>
        Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
    }
}


