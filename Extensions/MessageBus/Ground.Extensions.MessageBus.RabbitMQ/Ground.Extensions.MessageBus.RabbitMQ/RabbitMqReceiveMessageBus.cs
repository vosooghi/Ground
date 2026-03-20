using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.RabbitMQ.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using Ground.Extensions.MessageBus.RabbitMQ.Extensions;

namespace Ground.Extensions.MessageBus.RabbitMQ
{
    /// <summary>
    /// Provides a message bus implementation for receiving commands and events from RabbitMQ queues. Enables
    /// subscribing to event topics and receiving command messages for a service using RabbitMQ as the transport.
    /// </summary>
    /// <remarks>RabbitMqReceiveMessageBus manages the creation and binding of queues for commands and events
    /// based on service configuration. It supports dependency injection for message consumers and logs queue
    /// operations. The class is thread-safe for typical usage scenarios and should be disposed when no longer needed to
    /// release RabbitMQ resources.</remarks>
    public class RabbitMqReceiveMessageBus : IReceiveMessageBus, IAsyncDisposable, IDisposable
    {
        private readonly ILogger<RabbitMqReceiveMessageBus> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly IConnection _connection;
        private readonly string _eventQueueName;
        private readonly string _commandQueueName;
        private readonly IServiceScopeFactory _serviceScopeFactory;        
        private IChannel? _channel;
        private bool _isInitialized;

        public RabbitMqReceiveMessageBus(
            IConnection connection,
            ILogger<RabbitMqReceiveMessageBus> logger,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _connection = connection;

            _commandQueueName = $"{_rabbitMqOptions.ServiceName}.CommandsInputQueue";
            _eventQueueName = $"{_rabbitMqOptions.ServiceName}.EventsInputQueue";
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized) return; // Prevent double initialization

            _channel = await _connection.CreateChannelAsync();
            await _channel.ExchangeDeclareAsync(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);

            await CreateCommandQueueAsync();
            await CreateEventQueueAsync();

            _isInitialized = true;
        }

        private async Task CreateEventQueueAsync()
        {
            if (_channel == null) throw new InvalidOperationException("Channel is not initialized.");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += Consumer_EventReceivedAsync;

            var queue = await _channel.QueueDeclareAsync(_eventQueueName, true, false, false);
            await _channel.BasicConsumeAsync(queue.QueueName, true, consumer);

            _logger.LogInformation("Event Queue With Name {queueName} Created.", queue.QueueName);
        }

        private async Task CreateCommandQueueAsync()
        {
            if (_channel == null) throw new InvalidOperationException("Channel is not initialized.");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += Consumer_CommandReceivedAsync;

            var queue = await _channel.QueueDeclareAsync(_commandQueueName, true, false, false);
            await _channel.BasicConsumeAsync(queue.QueueName, true, consumer);

            _logger.LogInformation("Command Queue With Name {commandName} Created.", queue.QueueName);
        }

        public async Task Subscribe(string serviceId, string eventName)
        {
            if (_channel == null) throw new InvalidOperationException("Channel is not initialized.");

            var route = $"{serviceId}.{RabbitMqSendMessageBusConstants.@event}.{eventName}";
            await _channel.QueueBindAsync(_eventQueueName, _rabbitMqOptions.ExchangeName, route);
            _logger.LogInformation("ServiceId: {serviceId} With EventName: {eventName} Binded.", serviceId, eventName);
        }

        public async Task Receive(string commandName)
        {
            if (_channel == null) throw new InvalidOperationException("Channel is not initialized.");

            var route = $"{_rabbitMqOptions.ServiceName}.{RabbitMqSendMessageBusConstants.command}.{commandName}";
            await _channel.QueueBindAsync(_commandQueueName, _rabbitMqOptions.ExchangeName, route);
            _logger.LogInformation("Command With CommandName: {commandName} Binded.", commandName);
        }

        private async Task Consumer_EventReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            try
            {
                using Activity span = StartChildActivity(e);
                _logger.LogDebug("Event Received With RoutingKey: {RoutingKey}.", e.RoutingKey);
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();

                await consumer.ConsumeEvent(e.BasicProperties.AppId, e.ToParcel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task Consumer_CommandReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            try
            {
                using Activity span = StartChildActivity(e);
                _logger.LogDebug("Command Received With RoutingKey: {RoutingKey}.", e.RoutingKey);
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();

                await consumer.ConsumeCommand(e.BasicProperties.AppId, e.ToParcel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private Activity StartChildActivity(BasicDeliverEventArgs e)
        {
            var span = new Activity("RabbitMqCommandReceived");
            span.AddTag("ApplicationName", _rabbitMqOptions.ServiceName);
            if (e.BasicProperties != null && e.BasicProperties.Headers != null && e.BasicProperties.Headers.ContainsKey("TraceId") && e.BasicProperties.Headers.ContainsKey("SpanId"))
            {
                span.SetParentId($"00-{System.Text.Encoding.UTF8.GetString(e.BasicProperties.Headers["TraceId"] as byte[])}-{System.Text.Encoding.UTF8.GetString(e.BasicProperties.Headers["SpanId"] as byte[])}-00");
            }
            span.Start();
            return span;
        }
        
        public async ValueTask DisposeAsync()
        {
            if (_channel is not null) await _channel.CloseAsync();
            if (_connection is not null) await _connection.CloseAsync();
        }

        public void Dispose()
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
    }
}
