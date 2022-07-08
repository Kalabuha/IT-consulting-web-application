using DbContextProfi;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interfaces;
using Resources.Entities;

namespace Repositories
{
    internal class HeaderMenuSetRepository : BaseRepository<HeaderMenuSetEntity>, IHeaderMenuSetRepository
    {
        public HeaderMenuSetRepository(DbContextProfiСonnector context) : base(context) {}

        public async Task<HeaderMenuSetEntity[]> GetAllHeaderMenuEntitiesAsync()
        {
            var headerMenuSets = await Context.HeaderMenuSets
                .ToArrayAsync();

            return headerMenuSets;
        }

        public async Task<HeaderMenuSetEntity?> GetPostedHeaderMenuEntitiesAsync()
        {
            var usedHeaderMenuSets = await Context.HeaderMenuSets
                .FirstOrDefaultAsync(m => m.IsPostedOnThePage == true);

            return usedHeaderMenuSets;
        }
    }
}
