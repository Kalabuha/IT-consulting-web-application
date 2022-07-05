using Microsoft.AspNetCore.Mvc;
using Resources.Models;
using Resources.Enums;
using Services.ServiceResources;
using Services.Interfaces;

namespace WebAppForGuests.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITextAndImageService _textAndImageService;
        private readonly IApplicationService _applicationService;
        private readonly IDataValidationService _dataValidationService;


        public HomeController(
            ILogger<HomeController> logger,
            ITextAndImageService textAndImageService,
            IApplicationService applicationService,
            IDataValidationService dataValidationService)
        {
            _logger = logger;
            _textAndImageService = textAndImageService;
            _applicationService = applicationService;
            _dataValidationService = dataValidationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationModel>> SubmitApplication(ApplicationValidationResult? applicationValidationResult)
        {
            var textModel = await _textAndImageService.GetPublishedTextModelAsync();
            var imageModel = await _textAndImageService.GetPublishedImageModelAsync();

            ViewBag.HomePageText = await GetHomePageText(textModel);
            ViewBag.HomePageImage = GetHomePageImage(imageModel);

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
        public async Task<ActionResult<ApplicationModel>> SubmitApplication(ApplicationModel application)
        {
            if (application == null)
            {
                return RedirectToAction(nameof(SubmitApplication));
            }

            TrimApplicationModelData(application);

            var applicationValidationResult = _dataValidationService.GetApplicationValidationResult(application);
            if (applicationValidationResult.NameValidError != null ||
                applicationValidationResult.EmailValidError != null ||
                applicationValidationResult.MessageValidError != null)
            {
                return RedirectToAction(nameof(SubmitApplication), applicationValidationResult);
            }

            application.Number = await _applicationService.GetFreeApplicationNumber();
            await _applicationService.AddApplicationToDb(application);

            return RedirectToAction(nameof(MessageApplicationSent), new { applicationNumber = application.Number });
        }

        [HttpGet]
        public ActionResult<string> MessageApplicationSent(int applicationNumber)
        {
            ViewBag.ApplicationNumber = applicationNumber;
            return View();
        }

        private async Task<string> GetHomePageText(TextModel? publishedTextModel)
        {
            if (publishedTextModel != null &&
                !string.IsNullOrEmpty(publishedTextModel.Text) &&
                !string.IsNullOrWhiteSpace(publishedTextModel.Text))
            {
                return publishedTextModel.Text;
            }
            else
            {
                return await _textAndImageService.GetDefaultHomePageTextAsync();
            }
        }

        private string GetHomePageImage(ImageModel? publishedImageModel)
        {
            if (publishedImageModel != null &&
                !string.IsNullOrEmpty(publishedImageModel.ImageAsString) &&
                !string.IsNullOrWhiteSpace(publishedImageModel.ImageAsString))
            {
                return publishedImageModel.ImageAsString;
            }
            else
            {
                return _textAndImageService.GetDefaultHomePageImage();
            }
        }

        private void TrimApplicationModelData(ApplicationModel application)
        {
            application.GuestEmail = application.GuestEmail?.Trim();
            application.GuestEmail = application.GuestEmail?.Trim();
            application.GuestApplicationText = application.GuestApplicationText?.Trim();
        }
    }
}