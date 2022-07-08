using DbContextProfi;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interfaces;
using Resources.Entities;

namespace Repositories
{
    internal class HeaderSloganRepository : BaseRepository<HeaderSloganEntity>, IHeaderSloganRepository
    {
        public HeaderSloganRepository(DbContextProfiСonnector context) : base(context) {}

        public async Task<HeaderSloganEntity[]> GetAllMainPageSloganEntitiesAsync()
        {
            var slogans = await Context.HeaderSlogans
                .ToArrayAsync();

            return slogans;
        }

        public async Task<HeaderSloganEntity[]> GetUsedHeaderSloganEntitiesAsync()
        {
            var usedSlogans = await Context.HeaderSlogans
                .Where(s => s.IsUsed == true)
                .ToArrayAsync();

            return usedSlogans;
        }
    }
}
