using Microsoft.AspNetCore.Mvc;
using WebAppForGuests.Models;
using Services.Interfaces;

namespace WebAppForGuests.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IHeaderService _headerService;

        public ServicesController(IServiceService serviceService, IHeaderService headerService)
        {
            _serviceService = serviceService;
            _headerService = headerService;
        }

        // GET: ServicesController
        public async Task<ActionResult<ServicesViewModel>> Index()
        {
            var headerModel = await _headerService.GetPublishedHeaderModelAsync();
            ViewBag.PageH1 = headerModel.Services;

            var services = await _serviceService.GetPublishedServiceModelsAsync();

            return View(new ServicesViewModel { Services = services });
        }
    }
}
