namespace Messaging.ServiceBus
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Logging;

    public class ServiceBusQueueClient<T> : IQueueClient<T>
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger _logger;
        private readonly IMessageSender<T> _messageSender;

        public ServiceBusQueueClient(
            ServiceBusQueueSettings<T> settings,
            ILogger<ServiceBusQueueClient<T>> logger,
            IMessageSender<T> messageSender)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (!settings.IsValid)
            {
                _logger.LogError("Invalid Service Bus queue settings passed.");
                throw new ArgumentException($"Invalid {nameof(ServiceBusQueueSettings<T>)}", nameof(settings));
            }

            _messageSender = messageSender;
            _queueClient = new QueueClient(settings.ConnectionString, settings.QueueName);
        }

        public async Task SendAsync(T message)
        {
            await _messageSender.SendAsync(_queueClient, message);
        }
    }
}
