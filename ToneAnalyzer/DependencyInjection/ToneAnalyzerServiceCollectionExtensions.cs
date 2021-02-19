namespace ToneAnalyzerFunction.DependencyInjection
{
    using IBM.Cloud.SDK.Core.Authentication.Iam;
    using IBM.Watson.ToneAnalyzer.v3;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ToneAnalyzerFunction.Options;

    public static class ToneAnalyzerServiceCollectionExtensions
    {
        public static IServiceCollection AddToneAnalyzerServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IToneAnalyzerService>(sp =>
            {
                services.Configure<ToneAnalyzerOptions>(configuration.GetSection(ToneAnalyzerOptions.ToneAnalyzer));
                var toneAnalyzerOptions = configuration.GetSection(ToneAnalyzerOptions.ToneAnalyzer).Get<ToneAnalyzerOptions>();
                var authenticator = new IamAuthenticator(apikey: toneAnalyzerOptions.ApiKey);
                var toneAnalyzerService = new ToneAnalyzerService(toneAnalyzerOptions.Version, authenticator);
                toneAnalyzerService.SetServiceUrl(toneAnalyzerOptions.Endpoint);

                return toneAnalyzerService;
            });

            return services;
        }
    }
}
