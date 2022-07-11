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
            PresetInfo? currentPresetInfo;

            if (selectedPresetId.HasValue && selectedPresetId.Value > 0)
            {
                currentPresetInfo = await _mainPageService.GetPresetInfoByIdAsync(selectedPresetId.Value);
                ViewBag.PublishedMainPage = await _mainPageService.GetMainPageModelByIdAsync(selectedPresetId.Value);
            }
            else
            {
                currentPresetInfo = await _mainPageService.GetPublishedPresetInfoAsync();
                ViewBag.PublishedMainPage = await _mainPageService.GetPublishedMainPageModelAsync();
            }

            ViewBag.AllPresetsInfo = await _mainPageService.GetAllPresetInfosAsync();

            return View(currentPresetInfo);
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

        [HttpGet]
        public async Task<IActionResult> CreatePreset()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePreset()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeletePreset()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}