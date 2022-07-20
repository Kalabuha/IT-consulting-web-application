using Microsoft.AspNetCore.Mvc;
using WebAppForAdmins.Models;
using Services.Interfaces;
using Services.Converters;
using Resources.Models;

namespace WebAppForAdmins.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectDataValidationService _projectDataValidationService;

        public ProjectsController(IProjectService projectService, IProjectDataValidationService projectDataValidationService)
        {
            _projectService = projectService;
            _projectDataValidationService = projectDataValidationService;
        }

        // GET: ProjectsController
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProjectsViewModel()
            {
                Projects = await _projectService.GetAllProjectModelsAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(ProjectModel project)
        {
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(ProjectModel project)
        {
            if (project.CustomerCompanyLogoAsFormFile != null)
            {
                project.CustomerCompanyLogoAsString = ReadBytesFromFormFile(project.CustomerCompanyLogoAsFormFile);
            }

            var projectDataValidationResult = _projectDataValidationService.GetProjectValidationResult(project);

            if (projectDataValidationResult.IsDataNotVerified)
            {
                project.ProjectTitleValidationMessage = projectDataValidationResult.TitleValidError;
                project.ProjectDescriptionValidationMessage = projectDataValidationResult.DescriptionValidError;

                return RedirectToAction(nameof(Create), project);
            }

            await _projectService.AddProjectToDbAsync(project);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(ProjectModel project)
        {
            if (project.ProjectTitle == null && project.ProjectTitleValidationMessage == null ||
                project.ProjectDescription == null && project.ProjectDescriptionValidationMessage == null)
            {
                var editableProject = await _projectService.GetProjectByIdAsync(project.Id);
                if (editableProject == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(editableProject);
            }

            if (project.CustomerCompanyLogoAsFormFile != null)
            {
                project.CustomerCompanyLogoAsString = ReadBytesFromFormFile(project.CustomerCompanyLogoAsFormFile);
            }

            // oldProjectFromDb - просто для проверки - не пропал ли проект из БД, пока пользователь вносил изменения.
            var oldProjectFromDb = await _projectService.GetProjectByIdAsync(project.Id);
            if (oldProjectFromDb != null)
            {
                return View(project);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(ProjectModel project)
        {
            var projectDataValidationResult = _projectDataValidationService.GetProjectValidationResult(project);

            if (projectDataValidationResult.IsDataNotVerified)
            {
                project.ProjectTitleValidationMessage = projectDataValidationResult.TitleValidError;
                project.ProjectDescriptionValidationMessage = projectDataValidationResult.DescriptionValidError;

                return RedirectToAction(nameof(Edit), project);
            }

            await _projectService.EditProjectToDbAsync(project);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(ProjectModel project)
        {
            var removableProject = await _projectService.GetProjectByIdAsync(project.Id);
            if (removableProject == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(removableProject);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(ProjectModel project)
        {
            var removableProject = await _projectService.GetProjectByIdAsync(project.Id);
            if (removableProject != null)
            {
                await _projectService.RemoveProjectToDbAsync(removableProject);
            }

            return RedirectToAction(nameof(Index));
        }

        private string ReadBytesFromFormFile(IFormFile formFile)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)formFile.Length);
            }

            return DataConverter.Array64ToDataImageString(imageData);
        }
    }
}
