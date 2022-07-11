using Repositories.Interfaces;
using Resources.Models;
using Services.Converters;
using Services.Interfaces;

namespace Services
{
    internal class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<ContactModel>> GetAllContactModelsAsync()
        {
            var contacts = (await _contactRepository.GetAllContactEntitiesAsync())
                .Select(c => c.ContactEntityToModel())
                .ToList();

            return contacts;
        }

        public async Task<ContactModel?> GetPublishedContactModelAsync()
        {
            var contact = (await _contactRepository.GetAllContactEntitiesAsync())
                .FirstOrDefault(c => c.IsPublishedOnMainPage == true);

            return contact?.ContactEntityToModel();
        }

        public async Task<ContactModel?> GetContactByIdAsync(int projectId)
        {
            var contact = await _contactRepository.GetEntity(projectId);

            return contact?.ContactEntityToModel();
        }
    }
}
