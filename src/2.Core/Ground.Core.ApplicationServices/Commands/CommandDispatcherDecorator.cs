using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.RequestResponse.Commands;

namespace Ground.Core.ApplicationServices.Commands
{
    /// <summary>
    /// Defines an abstract base class for decorating command dispatchers, allowing additional behavior to be added to the command dispatching process.
    /// </summary>
    public abstract class CommandDispatcherDecorator : ICommandDispatcher
    {
        #region Fields
        protected ICommandDispatcher _commandDispatcher;
        public abstract int Order { get; }
        #endregion

        #region Constructors
        public CommandDispatcherDecorator(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        #endregion
        public void SetCommandDispatcher(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        #region Abstract Send Commands
        public abstract Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

        public abstract Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
        #endregion
    }

}
