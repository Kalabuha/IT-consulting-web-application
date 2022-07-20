using Services.ServiceResources;
using Resources.Models;

namespace Services.Interfaces
{
    public interface IProjectDataValidationService
    {
        public ProjectValidationResult GetProjectValidationResult(ProjectModel project);
    }
}
