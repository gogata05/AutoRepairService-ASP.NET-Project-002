using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult LiveChat()
        {
            return View();
        }
    }
}
