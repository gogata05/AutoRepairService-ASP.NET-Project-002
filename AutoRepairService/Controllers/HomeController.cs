using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoRepairService.Core.ViewModels;
using Microsoft.AspNetCore.Diagnostics;

namespace AutoRepairService.Controllers
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (feature != null)
            {
                var statusCode = HttpContext.Response.StatusCode;

                if (statusCode == 404)
                {
                    return View("404");
                }
                if (statusCode == 500)
                {
                    return View("500");
                }
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
