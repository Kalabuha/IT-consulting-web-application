using Resources.Models;

namespace Services.Interfaces
{
    public interface IContactService
    {
        public Task<List<ContactModel>> GetAllContactModelsAsync();
        public Task<ContactModel?> GetPublishedContactModelAsync();
        public Task<ContactModel?> GetContactByIdAsync(int projectId);
    }
}
