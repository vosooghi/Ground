using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.RabbitMQ.Options;
using Ground.Extensions.Serializers.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Diagnostics;

namespace Ground.Extensions.MessageBus.RabbitMQ
{
    /// <summary>
    /// Provides functionality to send messages to RabbitMQ. It allows you to publish events and send commands to specific services. The class manages the connection and channel to RabbitMQ, ensuring that messages are sent reliably. It also integrates with logging and supports activity tracing for better observability.
    /// </summary>
    public class RabbitMqSendMessageBus : ISendMessageBus, IAsyncDisposable, IDisposable
    {
        private IChannel? _channel;
        private readonly IConnection _connection;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<RabbitMqSendMessageBus> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public RabbitMqSendMessageBus(
            IConnection connection,
            IJsonSerializer jsonSerializer,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            ILogger<RabbitMqSendMessageBus> logger)
        {
            _connection = connection;
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
        }

        public async Task InitializeAsync()
        {
            _channel = await _connection.CreateChannelAsync();
            await _channel.ExchangeDeclareAsync(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);
        }

        public async Task Publish<TInput>(TInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            string messageName = input.GetType().Name;
            Parcel parcel = new()
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageBody = _jsonSerializer.Serialize(input),
                MessageName = messageName,
                Route = $"{_rabbitMqOptions.ServiceName}.{RabbitMqSendMessageBusConstants.@event}.{messageName}",
                Headers = new Dictionary<string, object?>
                {
                    ["AccuredOn"] = DateTime.Now.ToString(),
                }
            };
            await Send(parcel);
        }

        public async Task SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData)
        {
            if (commandData == null) throw new ArgumentNullException(nameof(commandData));

            Parcel parcel = new()
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageBody = _jsonSerializer.Serialize(commandData),
                MessageName = commandName,
                Route = $"{destinationService}.{RabbitMqSendMessageBusConstants.command}.{commandName}"
            };
            await Send(parcel);
        }

        public async Task SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData)
        {
            if (commandData == null) throw new ArgumentNullException(nameof(commandData));

            Parcel parcel = new()
            {
                MessageId = Guid.NewGuid().ToString(),
                CorrelationId = correlationId,
                MessageBody = _jsonSerializer.Serialize(commandData),
                MessageName = commandName,
                Route = $"{destinationService}.{RabbitMqSendMessageBusConstants.command}.{commandName}"
            };
            await Send(parcel);
        }

        public async Task Send(Parcel parcel)
        {
            if (parcel is null) throw new ArgumentNullException(nameof(parcel));
            if (_channel is null) throw new InvalidOperationException("Channel is not initialized. Call InitializeAsync() first.");

            using Activity childActivity = StartChildActivity(parcel);
            AddActivityHeaders(parcel, childActivity);

            var basicProperties = new BasicProperties
            {
                Persistent = _rabbitMqOptions.PersistMessage,
                AppId = _rabbitMqOptions.ServiceName,
                CorrelationId = parcel.CorrelationId,
                MessageId = parcel.MessageId,
                Headers = parcel.Headers,
                Type = parcel.MessageName
            };

            var body = System.Text.Encoding.UTF8.GetBytes(parcel.MessageBody);

            await _channel.BasicPublishAsync(
                exchange: _rabbitMqOptions.ExchangeName,
                routingKey: parcel.Route,
                mandatory: false,
                basicProperties: basicProperties,
                body: body);

            _logger.LogDebug("Message Sent {MessageName}", parcel.MessageName);
        }

        private static void AddActivityHeaders(Parcel parcel, Activity childActivity)
        {
            parcel.Headers ??= new Dictionary<string, object?>();

            parcel.Headers["TraceId"] = childActivity.TraceId.ToHexString();
            parcel.Headers["SpanId"] = childActivity.SpanId.ToHexString();
        }

        private Activity StartChildActivity(Parcel parcel)
        {
            var child = new Activity("SendParcel");
            child.AddTag("ParcelName", parcel.MessageName);
            child.AddTag("ApplicationName", _rabbitMqOptions.ServiceName);
            child.Start();
            return child;
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
            {
                if (_channel.IsOpen)
                {
                    await _channel.CloseAsync();
                }
                _channel.Dispose();
            }
        }

        public void Dispose()
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
    }
}
