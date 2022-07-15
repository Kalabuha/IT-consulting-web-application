using Resources.Models;
using Resources.Containers;

namespace Services.Interfaces
{
    public interface IMainPageService
    {
        public Task<List<PresetInfo>> GetAllPresetInfosAsync();
        public Task<PresetInfo?> GetPublishedPresetInfoAsync();
        public Task<PresetInfo?> GetPresetInfoByIdAsync(int presetId);

        public Task<List<MainPageModel>> GetAllMainPageModelsAsync();
        public Task<MainPageModel?> GetPublishedMainPageModelAsync();
        public Task<MainPageModel?> GetMainPageModelByIdAsync(int presetId);
        public Task<MainPageModel> GetDefaultMainPageModelAsync();

        public Task<MainPageAllDataContainer> GetMainPageAllData();

        //Preset
        public Task PublishPreset(int presetId);
        public Task DeletePreset(int presetId);
        public Task<int> CreatePreset(string presetName);

        //PresetElements
        public Task ChangePresetElement(int presetId, int elementId, Type modelType);
        public Task TryDeletePresetElement(int elementId, Type modelType);

    }
}
