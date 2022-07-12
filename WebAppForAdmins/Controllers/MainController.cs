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
            var mainViewModel = new MainViewModel();
            if (selectedPresetId.HasValue && selectedPresetId.Value > 0)
            {
                mainViewModel.MainPageModel = await _mainPageService.GetMainPageModelByIdAsync(selectedPresetId.Value);
            }
            else
            {
                mainViewModel.MainPageModel = await _mainPageService.GetPublishedMainPageModelAsync();
            }

            mainViewModel.PresetInfos = await _mainPageService.GetAllPresetInfosAsync();

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}