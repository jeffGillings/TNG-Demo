namespace ToneAnalyzerFunction.Services
{
    using System;
    using System.IO;
    using System.Text;
    using IBM.Cloud.SDK.Core.Authentication.Iam;
    using IBM.Cloud.SDK.Core.Http;
    using IBM.Watson.ToneAnalyzer.v3;
    using IBM.Watson.ToneAnalyzer.v3.Model;
    using Microsoft.Extensions.Options;
    using ToneAnalyzerFunction.Options;

    public class ToneAnalyzer : IToneAnalyzer
    {
        private ToneAnalyzerOptions _options;
        private IamAuthenticator Authenticator => new IamAuthenticator(apikey: _options.ApiKey);

        public ToneAnalyzer(IOptions<ToneAnalyzerOptions> toneAnalyzerOptions)
        {
            _options = toneAnalyzerOptions?.Value ?? throw new ArgumentNullException(nameof(toneAnalyzerOptions));
        }

        public string GetTone(string comment)
        {
            var toneAnalyzer = new ToneAnalyzerService(_options.Version, Authenticator);
            toneAnalyzer.SetServiceUrl(_options.Endpoint);

            DetailedResponse<ToneAnalysis> result = toneAnalyzer.Tone( MapToAnalyzerInput(comment) );

            return result.Response;
        }

        private MemoryStream MapToAnalyzerInput(string comment)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(comment));
        }
    }
}
