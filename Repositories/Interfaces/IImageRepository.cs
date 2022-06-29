using Resources.Entities;

namespace Repositories.Interfaces
{
    public interface IImageRepository : IRepository<HomePageImageEntity>
    {
        public Task<HomePageImageEntity[]> GetAllImageEntitiesAsync();
    }
}
