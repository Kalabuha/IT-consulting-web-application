using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly IHeaderService _headerService;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService, IHeaderService headerService)
        {
            _logger = logger;
            _projectService = projectService;
            _headerService = headerService;
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> Index()
        {
            var headerModel = await _headerService.GetPublishedHeaderModelAsync();
            ViewBag.PageH1 = headerModel.Projects;

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
