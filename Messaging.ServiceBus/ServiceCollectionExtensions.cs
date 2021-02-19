namespace Messaging.ServiceBus
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueueHandler<T>(
            this IServiceCollection services,
            ServiceBusQueueSettings<T> settings)
        {
            services.AddScoped(provider => settings);
            services.AddScoped(typeof(IReceiverSettings<T>), provider => settings);
            services.AddScoped(typeof(IMessageSender<T>), typeof(DefaultMessageSender<T>));
            services.AddScoped<IQueueClient<T>, ServiceBusQueueClient<T>>();

            return services;
        }
    }
}
