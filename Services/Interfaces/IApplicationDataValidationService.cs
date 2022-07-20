using Resources.Models;
using Services.ServiceResources;

namespace Services.Interfaces
{
    public interface IApplicationDataValidationService
    {
        public ApplicationValidationResult GetApplicationValidationResult(ApplicationModel application);
    }
}
