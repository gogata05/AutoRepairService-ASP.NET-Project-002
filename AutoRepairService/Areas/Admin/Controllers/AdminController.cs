
using AutoRepairService.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
