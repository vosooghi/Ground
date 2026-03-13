using Ground.Core.RequestResponse.Commands;

namespace Ground.Core.Contracts.ApplicationServices.Commands
{
    /// <summary>
    /// Defines the contract for handling commands.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TData">The type of the data returned by the command.</typeparam>
    public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
    {
        Task<CommandResult<TData>> Handle(TCommand request);
    }

    /// <summary>
    /// Defines the contract for handling commands that do not return data.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<CommandResult> Handle(TCommand request);
    }
}
