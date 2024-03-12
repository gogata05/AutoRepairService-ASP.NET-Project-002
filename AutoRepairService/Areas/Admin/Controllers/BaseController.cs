using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AutoRepairService.Core.Constants;

namespace AutoRepairService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class BaseController : Controller
    {

    }
}
