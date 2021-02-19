namespace Messaging.ServiceBus
{
    using System.Text;
    using System.Text.Json;
    using Microsoft.Azure.ServiceBus;

    public static class MessageExtensions
    {
        public static Message ToMessage<T>(this T message) =>
            new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)));
    }
}
