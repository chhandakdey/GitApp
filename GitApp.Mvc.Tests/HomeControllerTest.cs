using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Mvc.Controllers;
using GitApp.Mvc.Models;
using GitApp.Mvc.Transformers.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitApp.Mvc.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeController_IndexPost()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var transformer = new Mock<ITransformer>();
            var gitService = new Mock<IGitService>();

            var model = new GitAppInputViewModel();
            model.AccessToken = "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";
            model.RepoUrl = "https://github.com/octocat/hello-world.git";
            model.Username = "octocat";

            transformer.Setup(p => p.Transform<GitAppInputViewModel, GitRequestDTO>(model))
                .Returns(new GitRequestDTO
                {
                    AccessToken = model.AccessToken,
                    RepoUrl = model.RepoUrl,
                    Username = model.Username
                });

            IEnumerable<GitResultCommentDTO> returnDataGitService = new List<GitResultCommentDTO>
                {
                    new GitResultCommentDTO
                    {
                        Comment = "Hello World",
                        Id = 1,
                        SortedWords = "Hello(1), World(1)"
                    }
                };

            IEnumerable<GitResultCommentViewModel> returnDataTransform = new List<GitResultCommentViewModel>
                {
                    new GitResultCommentViewModel
                    {
                        Comment = "Hello World",
                        Id = 1,
                        SortedWords = "Hello(1), World(1)"
                    }
                };

            transformer.Setup(p => p.Transform<IEnumerable<GitResultCommentDTO>, IEnumerable<GitResultCommentViewModel>>(returnDataGitService))
                       .Returns(returnDataTransform);

            gitService.Setup(p => p.GetCommitMessagesAsync(transformer.Object
                .Transform<GitAppInputViewModel, GitRequestDTO>(model)))
                .Returns(Task.FromResult(returnDataGitService));

            var controllerObj = new HomeController(logger.Object, transformer.Object, gitService.Object);
            var resultData = controllerObj.Index(model).GetAwaiter().GetResult();
            Assert.NotNull(resultData);
        }
    }
}
