using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
    public class OfferController : Controller
    {
        [HttpPost]
        public IActionResult Send()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
