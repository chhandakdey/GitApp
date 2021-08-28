using GitApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new GitAppInputViewModel();
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while rendering Index Page => {ex.Message}");
                _logger.LogError(ex.StackTrace);
                _logger.LogDebug(ex.InnerException?.Message);
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Index(GitAppInputViewModel model)
        {
            try
            {
                //if (!ModelState.IsValid)
                    return View(model);

            }
            catch (Exception ex)
            {
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
