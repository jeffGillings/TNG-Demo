namespace Messaging.ServiceBus
{
    using System.Threading.Tasks;

    public interface IQueueClient<T>
    {
        Task SendAsync(T message);
    }
}
