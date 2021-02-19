using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ToneAnalyzerFunction;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ToneAnalyzerFunction
{
    using System;
    using Messaging.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ToneAnalyzerFunction.DependencyInjection;
    using ToneAnalyzerFunction.Options;
    using ToneAnalyzerFunction.Services;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            var configuration = new ConfigurationBuilder().BuildCustomisedConfiguration();

            builder.Services.Configure<CommentClientOptions>(configuration.GetSection(CommentClientOptions.CommentClient));

            builder.Services.AddToneAnalyzerServices(configuration);
            builder.Services.AddScoped<IToneAnalyzer, ToneAnalyzer>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IToneAnalyzer, ToneAnalyzer>();
            
            builder.Services.AddHttpClient<ICommentClient, CommentClient>();
            builder.Services.AddQueueHandler(configuration.Bind<ServiceBusQueueSettings<SetCommentToneMessage>>("CommentToneQueue"));
        }
    }
}
