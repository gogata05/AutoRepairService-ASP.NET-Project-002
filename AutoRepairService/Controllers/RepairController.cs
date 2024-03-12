using AutoRepairService.Core.Constants;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace AutoRepairService.Controllers
{

    [Authorize]
    public class RepairController : Controller
    {
        private readonly IRepairService service;
        private readonly ILogger<RepairController> logger;
        public RepairController(IRepairService _service, ILogger<RepairController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new RepairModel()
                {
                    RepairCategories = await service.AllCategories()
                };
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
        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Add(RepairModel model)
        {

            if (await service.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
            }

            if (!ModelState.IsValid)
            {
                model.RepairCategories = await service.AllCategories();
                return View(model);

            }
            try
            {
                var userId = User.Id();
                await service.AddRepairAsync(userId, model);
                TempData[MessageConstant.SuccessMessage] = "Repair send for review!";
                return RedirectToAction(nameof(MyRepairs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var repair = await service.GetAllRepairsAsync();
                return View(repair);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await service.GetEditAsync(id, User.Id());
                model.RepairCategories = await service.AllCategories();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(MyRepairs));
            }
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Edit(int id, RepairModel model)
        {
            try
            {
                if (await service.CategoryExists(model.CategoryId) == false)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
                    model.RepairCategories = await service.AllCategories();
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    model.RepairCategories = await service.AllCategories();
                    return View(model);

                }

                await service.PostEditAsync(id, model);
                return RedirectToAction(nameof(MyRepairs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(MyRepairs));
            }
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (await service.RepairExistAsync(id) == false)
                {
                    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                    return RedirectToAction("All", "Repair");
                }

                var model = await service.RepairDetailsAsync(id);
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> RepairOffers()
        {
            var model = await service.RepairOffersAsync(User.Id());

            return View(model);
        }

        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> MyRepairs()
        {
            try
            {
                var model = await service.GetMyRepairsAsync(User.Id());
                return View(model);
            }
            catch (Exception)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                string mechanicId = await service.CompleteRepair(id, User.Id());
                return RedirectToAction("RateMechanic", "Mechanic", new { id = mechanicId, repairId = id });
            }
            catch (Exception)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = $"{RoleConstants.Customer}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteRepairAsync(id, User.Id());
                return RedirectToAction(nameof(MyRepairs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                return RedirectToAction(nameof(MyRepairs));
            }
        }
    }
}
