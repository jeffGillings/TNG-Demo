namespace Messaging.ServiceBus
{
    public class ServiceBusQueueSettings<T> : IReceiverSettings<T>
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }

        public int MaxConcurrentCalls { get; set; } = 1;

        public bool AutoComplete { get; set; } = false;

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(ConnectionString)
            && !string.IsNullOrWhiteSpace(QueueName);
    }
}
