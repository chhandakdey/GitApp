using GitApp.Infrastructure.Adapters;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace GitApp.Infrastructure.Tests
{
    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;


        public DelegatingHandlerStub()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            var jsonFileContent = File.ReadAllText(@"C:\Users\cdey\source\repos\GitApp\GitApp.Infrastructure.Tests\Infra\GitHttpResponse.json");
            response.Content = new StringContent(jsonFileContent);
            _handlerFunc = (request, cancellationToken) => Task.FromResult(response);
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
    public class GitApiClientTest
    {
        [Fact]
        public void GitApiClient_SendAsync()
        {
            var logger = new Mock<ILogger<GitApiClient>>();

            var mockFactory = new Mock<IHttpClientFactory>();
            var configuration = new HttpConfiguration();
            var responseObj = new HttpResponseMessage();
            responseObj.StatusCode = HttpStatusCode.OK;
            var jsonFileContent = File.ReadAllText(@"C:\Users\cdey\source\repos\GitApp\GitApp.Infrastructure.Tests\Infra\GitHttpResponse.json");
            responseObj.Content = new StringContent(jsonFileContent);
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = responseObj;
                return Task.FromResult(response);
            });

            var client = new HttpClient(clientHandlerStub);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory factory = mockFactory.Object;

            var headers = new List<KeyValuePair<string, string>>();
            var byteArray = Encoding.ASCII.GetBytes($"username:password");
            var clientAuthrizationHeader = new AuthenticationHeaderValue("Basic",
                                                          Convert.ToBase64String(byteArray));
            headers.Add(new KeyValuePair<string, string>("Authorization", clientAuthrizationHeader.ToString()));            
            headers.Add(new KeyValuePair<string, string>("User-Agent", "GitApp"));


            var apiClient = new GitApiClient(mockFactory.Object, logger.Object);

            var result = apiClient.SendAsync("https://api.github.com/repos/octocat/hello-world/comments", HttpMethod.Get, headers, null)
                .GetAwaiter().GetResult();

            Assert.True(result.IsSuccessStatusCode);

        }
    }
}
