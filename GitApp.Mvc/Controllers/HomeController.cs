using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Mvc.Models;
using GitApp.Mvc.Transformers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransformer _transformer;
        private readonly IGitService _gitService;

        public HomeController(ILogger<HomeController> logger, ITransformer transformer, IGitService gitService)
        {
            _logger = logger;
            _transformer = transformer;
            _gitService = gitService;
        }

        public IActionResult Index()
        {
            ViewBag.Error = false;
            try
            {
                var model = new GitAppInputViewModel();
                model.AccessToken = "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";
                model.RepoUrl = "https://github.com/octocat/hello-world.git";
                model.Username = "octocat";
                return View(model);
            }
            catch(Exception ex)
            {
                ViewBag.Error = true;
                _logger.LogError($"Error while rendering Index Page => {ex.Message}");
                _logger.LogError(ex.StackTrace);
                _logger.LogDebug(ex.InnerException?.Message);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Index(GitAppInputViewModel model)
        {
            ViewBag.Error = false;
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var requestDTO = _transformer.Transform<GitAppInputViewModel, GitRequestDTO>(model);
                var result = await _gitService.GetCommitMessagesAsync(requestDTO);
                var resultViewModel = _transformer.Transform<IEnumerable<GitResultCommentDTO>, IEnumerable<GitResultCommentViewModel>>(result);
                return View("GitCommentResultView", resultViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                _logger.LogError($"Error while rendering Index Page => {ex.Message}");
                _logger.LogError(ex.StackTrace);
                _logger.LogDebug(ex.InnerException?.Message);
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
