using AutoRepairService.Core.Constants;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Mechanic;
using AutoRepairService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{


    [Authorize]
    public class MechanicController : Controller
    {
        private readonly IMechanicService service;
        private readonly ILogger<MechanicController> logger;

        public MechanicController(
            IMechanicService _service,
            ILogger<MechanicController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await service.AllMechanicsAsync();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Customer)]
        public IActionResult Become()
        {
            var model = new BecomeMechanicViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Customer)]
        public async Task<IActionResult> Become(BecomeMechanicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await service.AddMechanicAsync(User.Id(), model);
                return RedirectToAction("JoinMechanics", "User");
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult RateMechanic(string id, int repairId)
        {
            var model = new MechanicRatingModel()
            {
                MechanicId = id,
                UserId = User.Id(),
                RepairId = repairId
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RateMechanic(string id, int repairId, MechanicRatingModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await service.RateMechanicAsync(User.Id(), id, repairId, model);
                return RedirectToAction("MyRepairs", "Repair");
            }
            catch (Exception ms)
            {
                logger.LogError(ms.Message, ms);
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
