using AutoRepairService.Core.ViewModels.Admin;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Areas.Admin.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IRepository repo;

        public ChatController(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IActionResult> LiveChat(string id)
        {
            var model = await repo.AllReadonly<Message>()
                .Where(x => x.UserId == id)
                .Select(x => new MessageViewModel()
                {
                    UserId = x.UserId,
                    Id = x.Id,
                    ConnectionId = x.ConnectionId,
                    CreatedOn = x.CreatedOn

                }).FirstAsync();
            return View(model);
        }

        public async Task<IActionResult> MessageRequest()
        {
            var model = await repo.AllReadonly<Message>()
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new MessageViewModel()
                {
                    Id = x.Id,
                    ConnectionId = x.ConnectionId,
                    UserId = x.UserId,
                    CreatedOn = x.CreatedOn
                }).ToListAsync();
            return View(model);
        }
    }
}
