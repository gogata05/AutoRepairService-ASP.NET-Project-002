using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AutoRepairService.Controllers
{
    [Authorize]
    public class RepairController : Controller
    {
        private readonly IRepairService service;

        public RepairController(IRepairService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new RepairModel();
            var userId = User.Id();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RepairModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var userId = User.Id();

            await service.AddRepairAsync(userId, model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var repairs = await service.GetAllRepairsAsync();
            return View(repairs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await service.GetEditAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RepairModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            await service.PostEditAsync(id, model);
            return RedirectToAction("All", "Repair");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await service.RepairDetailsAsync(id);
            return View(model);
        }

    }
}
