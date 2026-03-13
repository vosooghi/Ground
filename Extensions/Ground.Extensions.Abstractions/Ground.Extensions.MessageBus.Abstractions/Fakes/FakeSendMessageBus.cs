

namespace Ground.Extensions.MessageBus.Abstractions.Fakes
{
    /// <summary>
    /// Provides a mock implementation of the message bus for sending and publishing messages without actual
    /// delivery. Intended for use in unit tests or development scenarios where message transport is simulated.
    /// </summary>
    public class FakeSendMessageBus : ISendMessageBus
    {        

        public FakeSendMessageBus()
        {
        }

        public Task Publish<TInput>(TInput input)
        {
            return Task.CompletedTask;
        }

        public Task Send(Parcel parcel)
        {
            return Task.CompletedTask;
        }

        public Task SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData)
        {
            return Task.CompletedTask;
        }

        public Task SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData)
        {
            return Task.CompletedTask;
        }
    }
}
