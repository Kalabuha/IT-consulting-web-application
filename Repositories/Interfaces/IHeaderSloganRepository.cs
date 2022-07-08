using Resources.Entities;

namespace Repositories.Interfaces
{
    public interface IHeaderSloganRepository : IRepository<HeaderSloganEntity>
    {
        public Task<HeaderSloganEntity[]> GetAllMainPageSloganEntitiesAsync();
        public Task<HeaderSloganEntity[]> GetUsedHeaderSloganEntitiesAsync();

    }
}
