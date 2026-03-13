namespace Ground.Extensions.MessageBus.Abstractions.Fakes
{
    /// <summary>
    /// Provides a mock implementation of the <see cref="IMessageConsumer"/> interface for consuming command and event
    /// messages in a simulated environment.
    /// </summary>
    public class FakeMessageConsumer : IMessageConsumer
    {
        public Task<bool> ConsumeCommand(string sender, Parcel parcel)
        {
            Consume("command", sender, parcel);
            return Task.FromResult(true);
        }

        public Task<bool> ConsumeEvent(string sender, Parcel parcel)
        {
            Consume("event", sender, parcel);
            return Task.FromResult(true);
        }

        private static Task Consume(string type, string sender, Parcel parcel)
        {
            // Parameters are intentionally unused in this fake implementation.
            return Task.CompletedTask;
        }
    }
}
