using Resources.Models;

namespace Services.Interfaces
{
    public interface IMainPageService
    {
        public Task<List<MainPageModel>> GetAllMainPageModelsAsync();
        public Task<MainPageModel> GetPublishedMainPageModelAsync();
    }
}
