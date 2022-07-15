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
        public async Task<IActionResult> Index(int? selectedPresetId)
        {
            MainPageModel? mainPageModel;
            if (selectedPresetId.HasValue && selectedPresetId.Value > 0)
            {
                mainPageModel = await _mainPageService.GetMainPageModelByIdAsync(selectedPresetId.Value);
            }
            else
            {
                mainPageModel = await _mainPageService.GetPublishedMainPageModelAsync();
            }

            mainPageModel ??= await _mainPageService.GetDefaultMainPageModelAsync();

            var presetInfos = await _mainPageService.GetAllPresetInfosAsync();

            var mainPageAllDataContainer = await _mainPageService.GetMainPageAllData();

            var mainViewModel = new MainViewModel()
            {
                MainPageModel = mainPageModel,
                PresetInfos = presetInfos,

                Actions = mainPageAllDataContainer.Actions,
                Buttons = mainPageAllDataContainer.Buttons,
                Images = mainPageAllDataContainer.Images,
                Phrases = mainPageAllDataContainer.Phrases,
                Texts = mainPageAllDataContainer.Texts,
            };

            return View(mainViewModel);
        }

        [HttpPost]
        public IActionResult ShowSelectedPreset(int presetId)
        {
            return RedirectToAction(nameof(Index), new { selectedPresetId = presetId });
        }

        [HttpPost]
        public async Task<IActionResult> PublishSelectedPreset(int presetId)
        {
            if (presetId > 0)
            {
                await _mainPageService.PublishPreset(presetId);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelectedPreset(int presetId)
        {
            if (presetId > 0)
            {
                await _mainPageService.DeletePreset(presetId);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSelectedPreset(string presetName)
        {
            var newPresetId = await _mainPageService.CreatePreset(presetName);

            return RedirectToAction(nameof(Index), new { selectedPresetId = newPresetId });
        }

        [HttpPost]
        public async Task<IActionResult> ActionForPresetElement(int presetId, int presetElementId, string action)
        {
            switch (action)
            {
                case "select-text":
                    await _mainPageService.ChangePresetElement(presetId, presetElementId, typeof(MainPageTextModel));
                    break;
                case "select-phrase":
                    await _mainPageService.ChangePresetElement(presetId, presetElementId, typeof(MainPagePhraseModel));
                    break;
                case "select-button":
                    await _mainPageService.ChangePresetElement(presetId, presetElementId, typeof(MainPageButtonModel));
                    break;
                case "select-image":
                    await _mainPageService.ChangePresetElement(presetId, presetElementId, typeof(MainPageImageModel));
                    break;
                case "select-action":
                    await _mainPageService.ChangePresetElement(presetId, presetElementId, typeof(MainPageActionModel));
                    break;
                case "delete-text":
                    await _mainPageService.TryDeletePresetElement(presetElementId, typeof(MainPageTextModel));
                    break;
                case "delete-phrase":
                    await _mainPageService.TryDeletePresetElement(presetElementId, typeof(MainPagePhraseModel));
                    break;
                case "delete-button":
                    await _mainPageService.TryDeletePresetElement(presetElementId, typeof(MainPageButtonModel));
                    break;
                case "delete-image":
                    await _mainPageService.TryDeletePresetElement(presetElementId, typeof(MainPageImageModel));
                    break;
                case "delete-action":
                    await _mainPageService.TryDeletePresetElement(presetElementId, typeof(MainPageActionModel));
                    break;
            }

            return RedirectToAction(nameof(Index), new { selectedPresetId = presetId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}