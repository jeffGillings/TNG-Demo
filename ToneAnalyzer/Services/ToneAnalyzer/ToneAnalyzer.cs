namespace ToneAnalyzerFunction.Services
{
    using System;
    using System.IO;
    using System.Text;
    using IBM.Cloud.SDK.Core.Http;
    using IBM.Watson.ToneAnalyzer.v3;
    using IBM.Watson.ToneAnalyzer.v3.Model;

    public class ToneAnalyzer : IToneAnalyzer
    {
        private IToneAnalyzerService _analyzerService;

        public ToneAnalyzer(IToneAnalyzerService analyzerService)
        {
            _analyzerService = analyzerService ?? throw new ArgumentNullException(nameof(analyzerService));
        }

        public string GetTone(string comment)
        {
            DetailedResponse<ToneAnalysis> result = _analyzerService.Tone( MapToAnalyzerInput(comment) );

            return result.Response;
        }

        private MemoryStream MapToAnalyzerInput(string comment)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(comment));
        }
    }
}
