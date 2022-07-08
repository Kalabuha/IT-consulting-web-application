using Microsoft.AspNetCore.Mvc;
using Resources.Models;
using Resources.Enums;
using Services.ServiceResources;
using Services.Interfaces;

namespace WebAppForGuests.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IMainPageService _mainPageService;
        private readonly IApplicationService _applicationService;
        private readonly IDataValidationService _dataValidationService;


        public MainController(
            ILogger<MainController> logger,
            IMainPageService mainPageService,
            IApplicationService applicationService,
            IDataValidationService dataValidationService)
        {
            _logger = logger;
            _mainPageService = mainPageService;
            _applicationService = applicationService;
            _dataValidationService = dataValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ApplicationValidationResult? applicationValidationResult)
        {
            var mainPageModel = await _mainPageService.GetPublishedMainPageModelAsync();
            ViewBag.MainPageModel = mainPageModel;

            if (applicationValidationResult == null)
            {
                ViewBag.NameErrorMessage = null;
                ViewBag.EmailErrorMessage = null;
                ViewBag.MessageErrorMessage = null;
            }
            else
            {
                ViewBag.NameErrorMessage = applicationValidationResult.NameValidError;
                ViewBag.EmailErrorMessage = applicationValidationResult.EmailValidError;
                ViewBag.MessageErrorMessage = applicationValidationResult.MessageValidError;
            }

            return View(new ApplicationModel
            {
                Status = ApplicationStatus.Initial,
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitApplication(ApplicationModel application)
        {
            if (application == null)
            {
                return RedirectToAction(nameof(Index));
            }

            TrimApplicationModelData(application);

            var applicationValidationResult = _dataValidationService.GetApplicationValidationResult(application);
            if (applicationValidationResult.NameValidError != null ||
                applicationValidationResult.EmailValidError != null ||
                applicationValidationResult.MessageValidError != null)
            {
                return RedirectToAction(nameof(Index), applicationValidationResult);
            }

            var applicationId = await _applicationService.AddApplicationToDb(application);
            return RedirectToAction(nameof(MessageApplicationSent), new { applicationId });
        }

        [HttpGet]
        public async Task<IActionResult> MessageApplicationSent(int applicationId)
        {
            var application = await _applicationService.GetApplicationByID(applicationId);
            return View(application);
        }

        private void TrimApplicationModelData(ApplicationModel application)
        {
            application.GuestEmail = application.GuestEmail?.Trim();
            application.GuestEmail = application.GuestEmail?.Trim();
            application.GuestApplicationText = application.GuestApplicationText?.Trim();
        }
    }
}