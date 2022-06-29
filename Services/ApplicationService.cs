using Repositories.Interfaces;
using Resources.Models;
using Services.Converters;
using Services.Interfaces;

namespace Services
{
    internal class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<List<ApplicationModel>> GetAllApplicationAsync()
        {
            var applications = (await _applicationRepository.GetApplicationsAsync())
                .Select(a => a.ApplicationEntityToModel())
                .ToList();

            return applications;
        }

        public async Task AddApplicationToDb(ApplicationModel newApplication)
        {
            if (newApplication == null ||
                string.IsNullOrEmpty(newApplication.GuestName) ||
                string.IsNullOrEmpty(newApplication.GuestEmail) ||
                string.IsNullOrEmpty(newApplication.GuestApplicationText))
            {
                return;
            }

            await _applicationRepository.AddEntityAsync(newApplication.ApplicationModelToEntity());
        }
    }
}
