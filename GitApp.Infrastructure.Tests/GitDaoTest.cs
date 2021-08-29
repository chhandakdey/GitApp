using GitApp.Application.DTOs;
using GitApp.Infrastructure.Daos;
using GitApp.Infrastructure.Repositories;
using GitApp.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitApp.Infrastructure.Tests
{
    public class GitDaoTest
    {
        [Fact]
        public void GitDao_GetAllAsync()
        {
            var logger = new Mock<ILogger<GitDao>>();

            var repo = new Mock<IGitRepository>();
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            var jsonFileContent = File.ReadAllText(@"../../../Infra/GitHttpResponse.json");
            response.Content = new StringContent(jsonFileContent);
            var request = new GitRequestDTO
            {
                AccessToken = "abcde",
                RepoUrl = "https://github.com/octocat/hello-world.git",
                Username = "octocat"
            };
            repo.Setup(p => p.GetAllCommentsAsync(request)).Returns(Task.FromResult(response));

            var daoObj = new GitDao(logger.Object, repo.Object);                       
            var returnedObj = daoObj.GetAllAsync(request).GetAwaiter().GetResult();
            foreach(var obj in returnedObj)
            {
                Assert.Equal(292400, obj.Id);
            }
        }
    }
}
