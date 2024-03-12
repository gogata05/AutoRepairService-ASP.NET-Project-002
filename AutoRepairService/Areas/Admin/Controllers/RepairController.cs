using AutoRepairService.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Areas.Admin.Controllers
{
    public class RepairController : BaseController
    {
        private readonly IRepairAdministrationService service;

        public RepairController(IRepairAdministrationService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var allRepairs = await service.GetAllRepairsAsync();

            return View(allRepairs);
        }

        public async Task<IActionResult> Active()
        {
            var repairs = await service.ReviewActiveRepairs();
            return View(repairs);
        }

        public async Task<IActionResult> Pending()
        {
            var repairs = await service.ReviewPendingRepairs();
            return View(repairs);
        }

        public async Task<IActionResult> Declined()
        {
            var repairs = await service.ReviewDeclinedRepairs();
            return View(repairs);
        }


        public async Task<IActionResult> Details(int id)
        {
            //if ((await service.RepairExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Repair");
            //}

            var model = await service.RepairDetailsAsync(id);
            return View(model);
        }

        public async Task<IActionResult> Approve(int id)
        {
            //if ((await service.RepairExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Repair");
            //}

            await service.ApproveRepairAsync(id);
            var repairs = await service.ReviewPendingRepairs();
            return View("Pending", repairs);
        }

        public async Task<IActionResult> Decline(int id)
        {
            //if ((await service.RepairExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Repair");
            //}

            await service.DeclineRepairAsync(id);
            var repairs = await service.ReviewPendingRepairs();
            return View("Pending", repairs);
        }


    }
}
