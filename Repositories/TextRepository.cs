using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Base;
using Resources.Entities;
using DbContextProfi;

namespace Repositories
{
    internal class TextRepository : BaseRepository<HomePageTextEntity>, ITextRepository
    {
        public TextRepository(DbContextProfiСonnector context) : base(context) { }

        public async Task<HomePageTextEntity[]> GetAllTextEntitiesAsync()
        {
            var texts = await Context.HomePageTexts
                .ToArrayAsync();

            return texts;
        }
    }
}