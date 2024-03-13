using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoRepairService.Core.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using AutoRepairService.Core.Constants;
using AutoRepairService.Core.IServices;

namespace AutoRepairService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICarService carService;

        public HomeController(ILogger<HomeController> _logger, ICarService _carService)
        {
            logger = _logger;
            carService = _carService;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await carService.GetLastThreeCars();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
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
