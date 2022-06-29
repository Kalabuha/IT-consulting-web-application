using Resources.Models;

namespace Services.Interfaces
{
    public interface IServiceService
    {
        public Task<List<ServiceModel>> GetAllServiceModelsAsync();
        public Task<List<ServiceModel>> GetPublishedServiceModelsAsync();

    }
}
