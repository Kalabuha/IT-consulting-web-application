using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> Index()
        {
            var projects = await _projectService.GetPublishedProjectModelsAsync();

            return View(new ProjectsViewModel { Projects = projects });
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> ProjectDetails(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            return View(new ProjectsViewModel { SelectedProject = project });
        }
    }
}
