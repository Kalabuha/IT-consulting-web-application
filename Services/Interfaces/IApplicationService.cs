using Resources.Models;

namespace Services.Interfaces
{
    public interface IApplicationService
    {
        public Task<List<ApplicationModel>> GetAllApplicationAsync();
        public Task AddApplicationToDb(ApplicationModel newApplication);
    }
}
