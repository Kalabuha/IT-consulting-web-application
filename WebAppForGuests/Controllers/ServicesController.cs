using Microsoft.AspNetCore.Mvc;
using WebAppForGuests.Models;
using Services.Interfaces;

namespace WebAppForGuests.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: ServicesController
        public async Task<ActionResult<ServicesViewModel>> Index()
        {
            var services = await _serviceService.GetPublishedServiceModelsAsync();

            return View(new ServicesViewModel { Services = services });
        }
    }
}
