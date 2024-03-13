using AutoRepairService.Core.Constants;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Areas.Admin.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService service;
        private readonly IAdminCarService adminService;
        private readonly ILogger<CarController> logger;

        public CarController(ICarService _service, IAdminCarService _adminService, ILogger<CarController> _logger)
        {
            service = _service;
            adminService = _adminService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new CarModel();
                model.CarCategories = await adminService.AllCategories();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(CarModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[MessageConstant.ErrorMessage] = "Invalid model data!";
                    model.CarCategories = await adminService.AllCategories();
                    return View(model);

                }

                await adminService.AddCarAsync(model, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Car added successfully";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var cars = await service.GetAllCarsAsync();
                return View(cars);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await adminService.GetEditAsync(id, User.Id());
                model.CarCategories = await adminService.AllCategories();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarModel model)
        {
            try
            {
                if (await adminService.CategoryExists(model.CategoryId) == false)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
                    model.CarCategories = await adminService.AllCategories();
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    model.CarCategories = await adminService.AllCategories();
                    return View(model);

                }

                await adminService.PostEditAsync(id, model);
                TempData[MessageConstant.SuccessMessage] = "Car edited successfully";

                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(All));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await adminService.RemoveCarAsync(id, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Car removed successfully";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                logger.LogError(ms.Message, ms);
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                return RedirectToAction(nameof(All));
            }
        }
    }
}
