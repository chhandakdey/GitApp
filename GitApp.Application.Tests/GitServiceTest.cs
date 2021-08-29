using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Application.Services;
using GitApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitApp.Application.Tests
{
    public class GitServiceTest
    {
        [Fact]
        public void GitService_GetCommitMessagesAsyncCount()
        {
            var asciiService = new Mock<IAsciiSortingService>();
            var dao = new Mock<IDao<GitRequestDTO, IEnumerable<GitCommentDTO>>>();
            var logger = new Mock<ILogger<GitService>>();

            var request = new GitRequestDTO
            {
                AccessToken = "abcde",
                RepoUrl = "https://github.com/octocat/hello-world.git",
                Username = "octocat"
            };            

            IEnumerable<GitCommentDTO> gitCommentDTOs = new List<GitCommentDTO>()
            {
                new GitCommentDTO()
                {
                    Id = 1,
                    Message = "Hello"
                },
                new GitCommentDTO()
                {
                    Id = 2,
                    Message = "Hello World"
                }
            };
            dao.Setup(p => p.GetAllAsync(request)).Returns(Task.FromResult(gitCommentDTOs));

            var result = new GitService(dao.Object, asciiService.Object, logger.Object)
                .GetCommitMessagesAsync(request).GetAwaiter().GetResult();

            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GitService_GetCommitMessagesAsyncSortedWords()
        {
            var asciiService = new Mock<IAsciiSortingService>();
            var dao = new Mock<IDao<GitRequestDTO, IEnumerable<GitCommentDTO>>>();
            var logger = new Mock<ILogger<GitService>>();

            var request = new GitRequestDTO
            {
                AccessToken = "abcde",
                RepoUrl = "https://github.com/octocat/hello-world.git",
                Username = "octocat"
            };

            IEnumerable<GitCommentDTO> gitCommentDTOs = new List<GitCommentDTO>()
            {
                new GitCommentDTO()
                {
                    Id = 2,
                    Message = "Hello World"
                }
            };
            dao.Setup(p => p.GetAllAsync(request)).Returns(Task.FromResult(gitCommentDTOs));
            Dictionary<string, int> values = new();
            values.Add("Hello", 1);
            values.Add("World", 1);

            asciiService.Setup(p => p.GetSorted("Hello World")).Returns(values);

            var result = new GitService(dao.Object, asciiService.Object, logger.Object)
                .GetCommitMessagesAsync(request).GetAwaiter().GetResult();

            Assert.Equal("Hello(1), World(1)", result.Where(p => p.Id == 2).FirstOrDefault().SortedWords);
        }
    }
}
