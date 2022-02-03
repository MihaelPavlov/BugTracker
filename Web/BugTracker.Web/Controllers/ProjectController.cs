namespace BugTracker.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> MyProjects()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ownerId = await this.projectService.GetOwnerIdByUserId(userId);

            if (!string.IsNullOrEmpty(ownerId))
            {
                var viewModel = await this.projectService.GetAllProjectByOwnerId(ownerId);
                return this.View(viewModel);
            }
            else if (string.IsNullOrEmpty(ownerId))
            {
                var employeeId = await this.projectService.GetEmployeeIdByUserId(userId);
                var viewModel = await this.projectService.GetAllProjectByEmployeeId(employeeId);
                return this.View(viewModel);
            }

            return this.BadRequest();
        }

        [HttpGet]
        public IActionResult LatestNews()
        {
            return this.View();
        }
    }
}
