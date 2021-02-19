namespace Messaging.ServiceBus
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;
    using Microsoft.Extensions.Logging;
    using System.Text.Json;

    internal class DefaultMessageSender<T> : IMessageSender<T>
    {
        private readonly ILogger _logger;

        public DefaultMessageSender(ILogger<DefaultMessageSender<T>> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(ISenderClient senderClient, T message)
        {
            var serialisedMessage = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await senderClient.SendAsync(new Message(serialisedMessage));

            _logger.LogInformation($"Message sent on : {senderClient.Path}, message: {serialisedMessage}");
        }
    }
}
