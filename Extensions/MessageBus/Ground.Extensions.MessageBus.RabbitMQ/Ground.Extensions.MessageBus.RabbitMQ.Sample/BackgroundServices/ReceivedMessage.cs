using Ground.Extensions.MessageBus.Abstractions;

namespace Ground.Extensions.MessageBus.RabbitMQ.Sample.BackgroundServices
{
    public class ReceivedMessage : IMessageConsumer
    {
        public async Task<bool> ConsumeCommand(string sender, Parcel parcel)
        {
            Console.WriteLine($"Message Command: {parcel.MessageName}");
            return true;
        }

        public async Task<bool> ConsumeEvent(string sender, Parcel parcel)
        {
             Console.WriteLine($"Message Event: {parcel.MessageName}");
            return true;
        }
    }
}
