using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
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
            var model = new AddRepairViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRepairViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            await service.AddRepairAsync(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var Repairs = await service.GetAllRepairsAsync();
            return View(Repairs);
        }
    }
}
}
