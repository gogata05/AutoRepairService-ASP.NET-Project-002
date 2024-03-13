using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoRepairService.Core.Constants;

namespace AutoRepairService.Controllers
{
    [AllowAnonymous]
    public class CarController : Controller
    {
        private readonly ICarService service;
        private readonly ILogger<CarController> logger;

        public CarController(ICarService _service, ILogger<CarController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllCarsQueryModel query)
        {
            try
            {
                var result = await service.AllCarsAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

                query.TotalCarsCount = result.TotalCarsCount;
                query.Categories = await service.AllCategoriesNames();
                query.Cars = result.Cars;

                return View(query);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //    try
        //    {
        //        var cars = await service.GetAllCarsAsync();
        //        return View(cars);
        //    }
        //    catch (Exception ms)
        //    {
        //        TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
        //        logger.LogError(ms.Message, ms);
        //        return RedirectToAction("Index", "Home");
        //    }          
        //}

    }
}
