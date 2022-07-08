using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Base;
using Resources.Entities;
using DbContextProfi;

namespace Repositories
{
    internal class MainPagePresetRepository : BaseRepository<MainPagePresetEntity>, IMainPagePresetRepository
    {
        public MainPagePresetRepository(DbContextProfiСonnector context) : base(context) {}

        public async Task<MainPagePresetEntity[]> GetAllMainPagePresetEntitiesAsync()
        {
            var presets = await Context.MainPagePresets
                .ToArrayAsync();

            return presets;
        }
    }
}
