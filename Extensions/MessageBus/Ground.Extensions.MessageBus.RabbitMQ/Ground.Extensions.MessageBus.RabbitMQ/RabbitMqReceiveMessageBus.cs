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
    public class RabbitMqReceiveMessageBus : IReceiveMessageBus, IDisposable
    {
        private readonly ILogger<RabbitMqReceiveMessageBus> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _eventQueueName;
        private readonly string _commandQueueName;
        private IServiceScopeFactory _serviceScopeFactory;

        public RabbitMqReceiveMessageBus(IConnection connection,
                                         ILogger<RabbitMqReceiveMessageBus> logger,
                                         IOptions<RabbitMqOptions> rabbitMqOptions,
                                         IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);
            _commandQueueName = $"{_rabbitMqOptions.ServiceName}.CommandsInputQueue";
            _eventQueueName = $"{_rabbitMqOptions.ServiceName}.EventsInputQueue";
            CreateCommandQueue();
            CreateEventQueue();
        }

        private void CreateEventQueue()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Consumer_EventReceived;
            var queue = _channel.QueueDeclare(_eventQueueName, true, false, false);
            _channel.BasicConsume(queue.QueueName, true, consumer);
            _logger.LogInformation("Event Queue With Name {queueName} Created.", queue.QueueName);
        }

        private void CreateCommandQueue()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_CommandReceived;
            var queue = _channel.QueueDeclare(_commandQueueName, true, false, false);
            _channel.BasicConsume(queue.QueueName, true, consumer);
            _logger.LogInformation("Command Queue With Name {commandName} Created.", queue.QueueName);
        }

        public async Task Subscribe(string serviceId, string eventName)
        {
            var route = $"{serviceId}.{RabbitMqSendMessageBusConstants.@event}.{eventName}";
            _channel.QueueBind(_eventQueueName, _rabbitMqOptions.ExchangeName, route);
            _logger.LogInformation("ServiceId: {serviceId} With EventName: {eventName} Binded.", serviceId, eventName);
        }

        public async Task Receive(string commandName)
        {
            var route = $"{_rabbitMqOptions.ServiceName}.{RabbitMqSendMessageBusConstants.command}.{commandName}";
            _channel.QueueBind(_commandQueueName, _rabbitMqOptions.ExchangeName, route);
            _logger.LogInformation("Command With CommandName: {commandName} Binded.", commandName);
        }

        private void Consumer_EventReceived(object sender, BasicDeliverEventArgs e)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            try
            {
                Activity span = StartChildActivity(e);
                _logger.LogDebug("Event Received With RoutingKey: {RoutingKey}.", e.RoutingKey);
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                consumer.ConsumeEvent(e.BasicProperties.AppId, e.ToParcel()).Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void Consumer_CommandReceived(object sender, BasicDeliverEventArgs e)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            try
            {
                Activity span = StartChildActivity(e);
                _logger.LogDebug("Command Received With RoutingKey: {RoutingKey}.", e.RoutingKey);
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                consumer.ConsumeCommand(e.BasicProperties.AppId, e.ToParcel()).Wait();
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

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
