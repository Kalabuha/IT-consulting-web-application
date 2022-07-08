using Resources.Entities;

namespace Repositories.Interfaces
{
    public interface IHeaderMenuSetRepository : IRepository<HeaderMenuSetEntity>
    {
        public Task<HeaderMenuSetEntity[]> GetAllHeaderMenuEntitiesAsync();
        public Task<HeaderMenuSetEntity?> GetPostedHeaderMenuEntitiesAsync();

    }
}
