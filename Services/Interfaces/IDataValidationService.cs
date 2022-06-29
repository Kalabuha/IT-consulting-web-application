using Resources.Models;
using Services.ServiceResources;

namespace Services.Interfaces
{
    public interface IDataValidationService
    {
        public ApplicationValidationResult GetApplicationValidationResult(ApplicationModel application);
    }
}
