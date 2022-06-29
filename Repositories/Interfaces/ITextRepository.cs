using Resources.Entities;

namespace Repositories.Interfaces
{
    public interface ITextRepository : IRepository<HomePageTextEntity>
    {
        public Task<HomePageTextEntity[]> GetAllTextEntitiesAsync();
    }
}
