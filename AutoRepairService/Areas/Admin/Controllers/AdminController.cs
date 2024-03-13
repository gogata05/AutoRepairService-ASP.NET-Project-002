using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Areas.Admin.Controllers
{

    public class AdminController : BaseController
    {

        public IActionResult Index()
        {

            return View();
        }

    }
}