using Repositories.Interfaces;
using Resources.Models;
using Services.Converters;
using Services.Interfaces;

namespace Services
{
    internal class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectModel>> GetAllProjectModelsAsync()
        {
            var projects = (await _projectRepository.GetAllProjectEntitiesAsync())
                .Select(obj => obj.ProjectEntityToModel())
                .ToList();

            return projects;
        }

        public async Task<List<ProjectModel>> GetPublishedProjectModelsAsync()
        {
            var projects = (await _projectRepository.GetAllProjectEntitiesAsync())
                .Where(p => p.IsPublished == true)
                .Select(obj => obj.ProjectEntityToModel())
                .ToList();

            return projects;
        }

        public async Task<ProjectModel?> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetEntity(projectId);

            return project?.ProjectEntityToModel();
        }
    }
}
