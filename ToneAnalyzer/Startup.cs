using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ToneAnalyzerFunction;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ToneAnalyzerFunction
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ToneAnalyzerFunction.Options;
    using ToneAnalyzerFunction.Services;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            var configuration = new ConfigurationBuilder().BuildCustomisedConfiguration();

            builder.Services.Configure<ToneAnalyzerOptions>(configuration.GetSection(ToneAnalyzerOptions.ToneAnalyzer));
            builder.Services.Configure<CommentClientOptions>(configuration.GetSection(CommentClientOptions.CommentClient));

            builder.Services.AddScoped<IToneAnalyzer, ToneAnalyzer>();
            builder.Services.AddScoped<ICommentClient, CommentClient>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IToneAnalyzer, ToneAnalyzer>();
        }
    }
}
