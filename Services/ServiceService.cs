using Repositories.Interfaces;
using Resources.Models;
using Services.Converters;
using Services.Interfaces;

namespace Services
{
    internal class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<ServiceModel>> GetAllServiceModelsAsync()
        {
            var services = (await _serviceRepository.GetAllServiceEntitiesAsync())
                .Select(s => s.ServiceEntityToModel())
                .ToList();

            return services;
        }

        public async Task<List<ServiceModel>> GetPublishedServiceModelsAsync()
        {
            var services = (await _serviceRepository.GetAllServiceEntitiesAsync())
                .Where(s => s.IsPublished == true)
                .Select(s => s.ServiceEntityToModel())
                .ToList();

            return services;
        }
    }
}
