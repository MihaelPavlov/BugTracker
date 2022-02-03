namespace BugTracker.Web.Controllers
{
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult CreateProject()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectInputModel createProjectInputModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this.projectService.CreateProject(userId, createProjectInputModel);
            if (result)
            {
                return this.Redirect($"/ProjectOptions/Overview");
            }
            else
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        public IActionResult MyProjects()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult LatestNews()
        {
            return this.View();
        }
    }
}
