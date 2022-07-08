using Resources.Entities;

namespace Repositories.Interfaces
{
    public interface IMainPagePresetRepository : IRepository<MainPagePresetEntity>
    {
        public Task<MainPagePresetEntity[]> GetAllMainPagePresetEntitiesAsync();

    }
}
