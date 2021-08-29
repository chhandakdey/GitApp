using GitApp.Application.DTOs;
using GitApp.Infrastructure.Adapters.Interfaces;
using GitApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitApp.Infrastructure.Tests
{
    public class GitRepositoryTest
    {
        [Fact]
        public void GitRepository_GetAllCommentsAsync()
        {
            var logger = new Mock<ILogger<GitRepository>>();
            var apiClient = new Mock<IApiClient>();
                        
            var inMemorySettings = new Dictionary<string, string> {
                        {"GitAppSettings:GitCommentUrl", "https://api.github.com/repos/{repo}/comments"},
                        {"GitAppSettings:GitRepoInitialUrl", "https://github.com/"},
                        {"GitAppSettings:GitRepoEndUrl", ".git"},
                        { "AcceptHeaderForComment","application/vnd.github.v3+json"}
            };
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var request = new GitRequestDTO
            {
                AccessToken = "abcde",
                RepoUrl = "https://github.com/octocat/hello-world.git",
                Username = "octocat"
            };

            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;            
            var jsonFileContent = File.ReadAllText(@"../../../Infra/GitHttpResponse.json");
            response.Content = new StringContent(jsonFileContent);

            apiClient.Setup(p => p.SendAsync(It.IsAny<string>(), HttpMethod.Get, It.IsAny<IEnumerable<KeyValuePair<string, string>>>(), null))
                .Returns(Task.FromResult(response));
            var gitRepo = new GitRepository(apiClient.Object, logger.Object, config);

            var resultData = gitRepo.GetAllCommentsAsync(request).GetAwaiter().GetResult();

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
