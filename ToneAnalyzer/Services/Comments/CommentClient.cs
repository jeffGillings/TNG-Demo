namespace ToneAnalyzerFunction.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using ToneAnalyzerFunction.Options;

    public class CommentClient : ICommentClient
    {
        private readonly HttpClient _client;
        private readonly CommentClientOptions _options;

        public CommentClient(HttpClient client, IOptions<CommentClientOptions> options)
        {
            _client = client;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _client.Timeout = _options.Timeout;
        }

        public async Task<bool> SetCommentTone(CommentTone toneUpdate)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(toneUpdate), Encoding.UTF8, "application/json");
                using HttpResponseMessage result = await _client.PutAsync(_options.EndPoint, content);

                return (result.StatusCode == HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
