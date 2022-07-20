using Resources.Models;
using Resources.Models.Base;

namespace Services.Interfaces
{
    public interface IMainPageService
    {
        public Task<List<MainPagePresetModel>> GetAllPresetModelsAsync();
        public Task<MainPagePresetModel?> GetPublishedPresetModelAsync();
        public Task<MainPagePresetModel?> GetPresetModelByIdAsync(int id);
        public MainPagePresetModel GetDefaultPresetModelAsync();
        public Task PublishPreset(int id);

        public Task<int> CreatePreset(MainPagePresetModel model);
        public Task UpdatePreset(MainPagePresetModel model);
        public Task DeletePreset(MainPagePresetModel model);

        public Task<MainPageTModel?> GetElementModelByIdAsync<MainPageTModel>(int id) where MainPageTModel : BaseModel;
        public Task<MainPageTModel?> GetElementModelByPresetIdAsync<MainPageTModel>(int id) where MainPageTModel : BaseModel;
        public Task<List<MainPageTModel>> GetAllElementModelsAsync<MainPageTModel>() where MainPageTModel : BaseModel;

        public Task<MainPageTextModel> GetDefaultMainPageTextModel();
        public Task<MainPageActionModel> GetDefaultMainPageActionModel();

    }
}
