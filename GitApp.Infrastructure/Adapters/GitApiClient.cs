using GitApp.Infrastructure.Adapters.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Infrastructure.Adapters
{
    /// <summary>
    /// This Api Client is used to talk to Github
    /// </summary>
    public class GitApiClient : IApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<GitApiClient> _logger;

        public GitApiClient(IHttpClientFactory clientFactory, ILogger<GitApiClient> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendAsync(string targetUrl, HttpMethod httpMethod,
            IEnumerable<KeyValuePair<string, string>> headers = null, HttpContent httpContent = null)
        {
            _logger.LogDebug($"Start => GitApiClient: SendAsync => Api Get call: {targetUrl}");
            var request = new HttpRequestMessage(httpMethod,
            targetUrl);
            var client = _clientFactory.CreateClient();
            if (headers != null && headers.Any())
            {
                headers.ToList().ForEach(x =>
                {
                    if (request.Headers.Contains(x.Key))
                    {
                        request.Headers.Remove(x.Key);
                    }
                    request.Headers.Add(x.Key, x.Value);
                });
            }
            if (httpContent != null && httpMethod != HttpMethod.Get)
            {
                request.Content = httpContent;
            }
            var response = await client.SendAsync(request);
            _logger.LogDebug($"End => GitApiClient: SendAsync => Api Get call: Status: {response.StatusCode}");
            return response.EnsureSuccessStatusCode();
        }
    }
}
