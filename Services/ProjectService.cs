using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using Resources.Models;
using Services.Common;
using Services.Converters;
using Services.Interfaces;

namespace Services
{
    internal class ProjectService : DefaultDataService, IProjectService
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

        public async Task AddProjectToDbAsync(ProjectModel project)
        {
            if (string.IsNullOrEmpty(project.CustomerCompanyLogoAsString))
            {
                var pathToDefaultCompanyLogo = GetDefaultImageFromFile("retro-wave-logo.png");
                var defaultCompanyLogoAsArray64 = DataConverter.PathToImageToArray64(pathToDefaultCompanyLogo);
                project.CustomerCompanyLogoAsString = DataConverter.Array64ToDataImageString(defaultCompanyLogoAsArray64);
            }

            project.ProjectTitle = DataConverter.CutTextByParameterIfNullReturnEmpty(project.ProjectTitle, 150);
            project.ProjectDescription = DataConverter.CutTextByParameterIfNullReturnEmpty(project.ProjectDescription, 5000);
            project.LinkToCustomerSite = DataConverter.CutTextByParameterIfNullReturnEmpty(project.LinkToCustomerSite, 300);

            var entity = project.ProjectModelToEntity();

            await _projectRepository.AddEntityAsync(entity);
        }

        public async Task EditProjectToDbAsync(ProjectModel project)
        {
            project.ProjectTitle = DataConverter.CutTextByParameterIfNullReturnEmpty(project.ProjectTitle, 150);
            project.ProjectDescription = DataConverter.CutTextByParameterIfNullReturnEmpty(project.ProjectDescription, 5000);
            project.LinkToCustomerSite = DataConverter.CutTextByParameterIfNullReturnEmpty(project.LinkToCustomerSite, 300);

            var entity = project.ProjectModelToEntity();

            await _projectRepository.UpdateEntityAsync(entity);
        }

        public async Task RemoveProjectToDbAsync(ProjectModel project)
        {
            var entity = await _projectRepository.GetEntity(project.Id);
            if (entity != null)
            {
                await _projectRepository.RemoveEntityAsync(entity);
            }
        }
    }
}
