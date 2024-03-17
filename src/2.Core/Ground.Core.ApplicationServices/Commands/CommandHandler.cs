using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.RequestResponse.Commands;
using Ground.Core.RequestResponse.Common;
using Ground.Utilities;

namespace Ground.Core.ApplicationServices.Commands
{

    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
        where TCommand : ICommand<TData>
    {

        protected readonly GroundServices _groundServices;
        protected readonly CommandResult<TData> result = new();
        public CommandHandler(GroundServices groundServices)
        {
            _groundServices = groundServices;
        }

        public abstract Task<CommandResult<TData>> Handle(TCommand command);
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Ok(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }
        protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual CommandResult<TData> Result(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }

        protected void AddMessage(string message)
        {
            result.AddMessage(_groundServices.Translator[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_groundServices.Translator[message, arguments]);
        }
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        protected readonly GroundServices _groundServices;
        protected readonly CommandResult result = new();
        public CommandHandler(GroundServices groundServices)
        {
            _groundServices = groundServices;
        }
        public abstract Task<CommandResult> Handle(TCommand command);

        protected virtual Task<CommandResult> OkAsync()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }

        protected virtual CommandResult Ok()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }

        protected virtual Task<CommandResult> ResultAsync(ApplicationServiceStatus status)
        {
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult Result(ApplicationServiceStatus status)
        {
            result.Status = status;
            return result;
        }
        protected void AddMessage(string message)
        {
            result.AddMessage(_groundServices.Translator[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_groundServices.Translator[message, arguments]);
        }
    }

}
