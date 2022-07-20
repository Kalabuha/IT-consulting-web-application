using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppForAdmins.Models;
using Services.Interfaces;
using Resources.Models;

namespace WebAppForAdmins.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IMainPageService _mainPageService;

        public MainController(ILogger<MainController> logger, IMainPageService mainPageService)
        {
            _logger = logger;
            _mainPageService = mainPageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.AllPresetModels = await _mainPageService.GetAllPresetModelsAsync();

            MainPagePresetModel? preset;
            if (id.HasValue && id.Value > 0)
            {
                preset = await _mainPageService.GetPresetModelByIdAsync(id.Value);
            }
            else
            {
                preset = await _mainPageService.GetPublishedPresetModelAsync();
            }

            preset ??= _mainPageService.GetDefaultPresetModelAsync();

            return View(preset);
        }

        [HttpPost]
        public IActionResult SelectPreset(MainPagePresetModel preset)
        {
            return RedirectToAction(nameof(Index), new { id = preset.Id });
        }

        [HttpPost]
        public async Task<IActionResult> PublishSelectedPreset(MainPagePresetModel preset)
        {
            if (preset.Id > 0)
            {
                await _mainPageService.PublishPreset(preset.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelectedPreset(MainPagePresetModel preset)
        {
            if (preset.Id > 0)
            {
                await _mainPageService.DeletePreset(preset);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePreset(MainPagePresetModel preset)
        {
            if (string.IsNullOrEmpty(preset.PresetName) || string.IsNullOrWhiteSpace(preset.PresetName))
            {
                return RedirectToAction(nameof(Index));
            }

            var id = await _mainPageService.CreatePreset(preset);

            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> ActionBlock(int id)
        {
            ViewBag.PresetId = id;

            if (id > 0)
            {
                var action = await _mainPageService.GetElementModelByPresetIdAsync<MainPageActionModel>(id);
                return PartialView(action);
            }
            else
            {
                var defaultActiont = await _mainPageService.GetDefaultMainPageActionModel();
                return PartialView(defaultActiont);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImageBlock(int id)
        {
            ViewBag.PresetId = id;

            ViewBag.AllButtons = await _mainPageService.GetAllElementModelsAsync<MainPageButtonModel>();
            ViewBag.AllImages = await _mainPageService.GetAllElementModelsAsync<MainPageImageModel>();
            ViewBag.AllPhrases = await _mainPageService.GetAllElementModelsAsync<MainPagePhraseModel>();

            var buttonModel = await _mainPageService.GetElementModelByPresetIdAsync<MainPageButtonModel>(id);
            var imageModel = await _mainPageService.GetElementModelByPresetIdAsync<MainPageImageModel>(id);
            var phraseModel = await _mainPageService.GetElementModelByPresetIdAsync<MainPagePhraseModel>(id);

            var imageBlockModel = new MainPageImageBlockModel
            {
                ButtonModel = buttonModel,
                ImageModel = imageModel,
                PhraseModel = phraseModel
            };

            return PartialView(imageBlockModel);
        }

        [HttpPost]
        public async Task<IActionResult> SelectButtonForCurrentPreset(MainPageButtonModel model, int id)
        {
            ViewBag.PresetId = id;

            var preset = await _mainPageService.GetPresetModelByIdAsync(id);
            var buttonModel = await _mainPageService.GetElementModelByIdAsync<MainPageButtonModel>(model.Id);

            if (preset != null && buttonModel != null)
            {
                preset.ButtonId = buttonModel.Id;
                await _mainPageService.UpdatePreset(preset);
            }

            return PartialView(buttonModel);
        }

        [HttpGet]
        public async Task<IActionResult> TextBlock(int id)
        {
            ViewBag.PresetId = id;
            if (id > 0)
            {
                var mainPageTextModel = await _mainPageService.GetElementModelByPresetIdAsync<MainPageTextModel>(id);
                return PartialView(mainPageTextModel);
            }
            else
            {
                var DefaultMainPageTextModel = await _mainPageService.GetDefaultMainPageTextModel();
                return PartialView(DefaultMainPageTextModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}