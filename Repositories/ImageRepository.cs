using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Base;
using Resources.Entities;
using DbContextProfi;

namespace Repositories
{
    internal class ImageRepository : BaseRepository<HomePageImageEntity>, IImageRepository
    {
        public ImageRepository(DbContextProfiСonnector context) : base(context) { }

        public async Task<HomePageImageEntity[]> GetAllImageEntitiesAsync()
        {
            var images = await Context.HomePageImages
                .ToArrayAsync();

            return images;
        }
    }
}
