using Repositories.Interfaces;
using Resources.Models;
using Resources.Enums;
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

        public async Task<List<ApplicationModel>> GetAllApplicationsAsync()
        {
            var applications = (await _applicationRepository.GetApplicationsAsync())
                .Select(a => a.ApplicationEntityToModel())
                .ToList();

            return applications;
        }

        public async Task<List<ApplicationModel>> GetFilteredApplications(ApplicationStatus[] statuses, DateTime start, DateTime end)
        {
            var applications = (await _applicationRepository.GetApplicationsAsync())
                .Where(a => statuses.Contains(a.Status) && a.DateReceipt >= start && a.DateReceipt <= end)
                .Select(a => a.ApplicationEntityToModel())
                .ToList();

            return applications;
        }

        public async Task<ApplicationModel?> GetApplicationByID(int id)
        {
            var application = (await _applicationRepository.GetApplicationsAsync())
                .FirstOrDefault(a => a.Id == id);

            return application?.ApplicationEntityToModel();
        }

        public async Task<int> GetFreeApplicationNumber()
        {
            var applications = await GetAllApplicationsAsync();
            var allNumbers = applications.Select(a => a.Number).ToArray();

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (!allNumbers.Contains(i))
                {
                    return i;
                }
            }

            throw new ApplicationException("Свободниые номера закончились");
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
