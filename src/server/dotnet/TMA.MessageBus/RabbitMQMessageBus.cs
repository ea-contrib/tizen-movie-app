using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TMA.MessageBus
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly RabbitMQMessageBusOptions _options;
        private readonly ILogger<RabbitMQMessageBus> _logger;
        private readonly IConnection _connection;

        public RabbitMQMessageBus(RabbitMQMessageBusOptions options, ILogger<RabbitMQMessageBus> logger = null)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;

            var connectionParams = ConnectionStringParams.FromString(options.ConnectionString);

            var factory = new ConnectionFactory
            {
                HostName = connectionParams.HostName,
                AutomaticRecoveryEnabled = options.AutomaticRecoveryEnabled,
                DispatchConsumersAsync = options.DispatchConsumersAsync,
                VirtualHost = connectionParams.VirtualHost,
                Port = connectionParams.Port,
                UserName = connectionParams.UserName,
                Password = connectionParams.Password,
                UseBackgroundThreadsForIO = options.UseBackgroundThreadsForIO,
            };

            _connection = factory.CreateConnection();
        }

        private string GetQueueName(Type type)
        {
            if (type.FullName == null)
            {
                throw new InvalidOperationException($"type.FullName of {type} is empty");
            }

            return type.FullName.ToUpperInvariant();
        }

        private string GetReplyQueueName(Type type)
        {
            var queueName = GetQueueName(type);

            return $"{queueName}_{Guid.NewGuid()}_reply";
        }

        public Task PublishAsync<TRequest>(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: GetQueueName(typeof(TRequest)),
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = _options.MessageSerializer.Serialize(request);

                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(exchange: string.Empty,
                    routingKey: GetQueueName(typeof(TRequest)),
                    basicProperties: null,
                    body: body);
            }

            return Task.CompletedTask;
        }

        public async Task<TResult> PublishAsync<TRequest, TResult>(TRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var tcs = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);
            string replyTo = GetReplyQueueName(typeof(TRequest));
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: GetQueueName(typeof(TRequest)),
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = _options.MessageSerializer.Serialize(request);

                var body = Encoding.UTF8.GetBytes(message);
                var props = channel.CreateBasicProperties();

                props.ReplyTo = replyTo;
                props.CorrelationId = Guid.NewGuid().ToString().ToUpperInvariant();

                channel.QueueDeclare(queue: props.ReplyTo,
                    durable: false,
                    exclusive: false,
                    autoDelete: true,
                    arguments: null);
                channel.BasicPublish(exchange: string.Empty,
                    routingKey: GetQueueName(typeof(TRequest)),
                    basicProperties: props,
                    body: body);
                channel.Close();
            }


            using (var responseChannel = _connection.CreateModel())
            {
                var consumer = new AsyncEventingBasicConsumer(responseChannel);

                Task OnReceive(object sender, BasicDeliverEventArgs @event)
                {
                    try
                    {
                        var responseBody = @event.Body;
                        var responseMessage = Encoding.UTF8.GetString(responseBody);
                        var responseMessageObj = _options.MessageSerializer.Deserialize<TResult>(responseMessage);

                        tcs.SetResult(responseMessageObj);
                    }
                    catch (Exception e)
                    {
                        _logger?.LogError(e, "error while request processing");
                        tcs.SetException(e);
                    }

                    consumer.Received -= OnReceive;

                    return Task.CompletedTask;
                }

                ;

                consumer.Received += OnReceive;
                ;

                responseChannel.BasicConsume(queue: replyTo,
                    autoAck: true,
                    consumer: consumer);


                if (cancellationToken == default)
                {
                    cancellationToken = new CancellationTokenSource(_options.RequestTimeout).Token;
                }

                var result = await tcs.WaitAsync(cancellationToken).ConfigureAwait(false);
                
                responseChannel.Close();

                return result;
            }
               
            
            
        }

        public Task<TResult> PublishAsync<TRequest, TResult>(CancellationToken cancellationToken = default)
            where TRequest: new()
        {
            return PublishAsync<TRequest, TResult>(new TRequest(), cancellationToken);
        }

        public Task SubscribeAsync<TRequest, TResult>(Func<TRequest, Task<TResult>> process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: GetQueueName(typeof(TRequest)),
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString(body);
                    var requestMessage = _options.MessageSerializer.Deserialize<TRequest>(message);


                    var response = await process(requestMessage).ConfigureAwait(true);

                    var responseText = response != null ? Encoding.UTF8.GetBytes(_options.MessageSerializer.Serialize(response)) : null;

                    using (var responseChannel = _connection.CreateModel())
                    {
                        var queueName = ea.BasicProperties.ReplyTo;
                        responseChannel.QueueDeclare(queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: true,
                            arguments: null);

                        var props = responseChannel.CreateBasicProperties();

                        props.CorrelationId = ea.BasicProperties.CorrelationId;

                        responseChannel.BasicPublish(exchange: string.Empty,
                            routingKey: queueName,
                            basicProperties: props,
                            body: responseText);
                    }
                }
                catch (Exception e)
                {
                    _logger?.LogError(e, "error while request processing");
                }
            };

            channel.BasicConsume(queue: GetQueueName(typeof(TRequest)),
                autoAck: true,
                consumer: consumer);


            return Task.CompletedTask;
        }


        public Task SubscribeAsync<TRequest>(Func<TRequest, Task> process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: GetQueueName(typeof(TRequest)),
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString(body);
                    var requestMessage = _options.MessageSerializer.Deserialize<TRequest>(message);


                    await process(requestMessage).ConfigureAwait(true);
                }
                catch (Exception e)
                {
                    _logger?.LogError(e, "error while request processing");
                }
            };

            channel.BasicConsume(queue: GetQueueName(typeof(TRequest)),
                autoAck: true,
                consumer: consumer);


            return Task.CompletedTask;
        }
    }
}