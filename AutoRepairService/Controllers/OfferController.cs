using AutoRepairService.Core.Constants;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Offer;
using AutoRepairService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AutoRepairService.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private readonly IOfferService service;
        public OfferController(IOfferService _service)
        {
            service = _service;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Mechanic)]
        public IActionResult Send()
        {
            var model = new OfferViewModel();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = RoleConstants.Mechanic)]
        public async Task<IActionResult> Send(int id, OfferViewModel model)
        {
            await service.SendOfferAsync(model, id, User.Id());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyOffers()
        {
            var offers = await service.MyOffersAsync(User.Id());

            return View(offers);

        }

        [HttpGet]
        public async Task<IActionResult> OffersCondition()
        {
            var offersCondition = await service.OffersConditionAsync(User.Id());

            return View(offersCondition);
        }

        [HttpGet]
        public async Task<IActionResult> Review(int id)
        {
            var model = await service.ReviewOfferAsync(id);

            return View(model);
        }

        public async Task<IActionResult> Accept(int id)
        {
            if (await service.OfferExists(id))
            {
                var offer = await service.GetOfferAsync(id);
                await service.AcceptOfferAsync(offer);
            }

            return RedirectToAction(nameof(MyOffers));

        }


        public async Task<IActionResult> Decline(int id)
        {
            if (await service.OfferExists(id))
            {
                var offer = await service.GetOfferAsync(id);
                await service.DeclineOfferAsync(offer);
            }
            return RedirectToAction(nameof(MyOffers));
        }
    }
}
