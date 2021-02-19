namespace Messaging.ServiceBus
{
    public interface IReceiverSettings<T>
    {
        int MaxConcurrentCalls { get; set; }

        bool AutoComplete { get; set; }
    }
}
