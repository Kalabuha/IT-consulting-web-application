using Resources.Models;

namespace Services.Interfaces
{
    public interface IMainPageService
    {
        public Task<List<PresetInfo>> GetAllPresetInfosAsync();
        public Task<PresetInfo?> GetPublishedPresetInfoAsync();
        public Task<PresetInfo?> GetPresetInfoByIdAsync(int presetId);
        public Task<List<MainPageModel>> GetAllMainPageModelsAsync();
        public Task<MainPageModel> GetPublishedMainPageModelAsync();
        public Task<MainPageModel> GetMainPageModelByIdAsync(int presetId);
        public Task PublishPreset(int presetId);

    }
}
