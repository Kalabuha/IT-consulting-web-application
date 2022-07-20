using Resources.Models;

namespace Services.Interfaces
{
    public interface IProjectService
    {
        public Task<List<ProjectModel>> GetAllProjectModelsAsync();
        public Task<List<ProjectModel>> GetPublishedProjectModelsAsync();
        public Task<ProjectModel?> GetProjectByIdAsync(int projectId);
        public Task AddProjectToDbAsync(ProjectModel project);
        public Task EditProjectToDbAsync(ProjectModel project);
        public Task RemoveProjectToDbAsync(ProjectModel project);

    }
}
