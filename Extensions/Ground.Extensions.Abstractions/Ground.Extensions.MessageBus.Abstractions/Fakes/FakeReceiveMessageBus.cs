
namespace Ground.Extensions.MessageBus.Abstractions.Fakes
{
    /// <summary>
    /// Provides a mock implementation of the message bus for receiving messages and subscribing to events.
    /// Intended for use in scenarios where actual message bus functionality is not required, such as unit testing or
    /// development environments.
    /// </summary>
    public class FakeReceiveMessageBus : IReceiveMessageBus
    {                
        public Task Receive(string commandName)
        {
            return Task.CompletedTask;
        }

        public Task Subscribe(string serviceId, string eventName)
        {
            return Task.CompletedTask;
        }
        
    }
}
