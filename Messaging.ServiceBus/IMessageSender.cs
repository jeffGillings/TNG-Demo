namespace Messaging.ServiceBus
{
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus.Core;

    public interface IMessageSender<in T>
    {
        Task SendAsync(ISenderClient senderClient, T message);
    }
}
