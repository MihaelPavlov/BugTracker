namespace BugTracker.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTracker.Common;
    using BugTracker.Data.Enums;
    using BugTracker.Services.Data;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IMemoryCache memoryCache;
        private readonly IEmailSender emailSender;

        public ProjectController(
            IProjectService projectService,
            IMemoryCache memoryCache,
            IEmailSender emailSender)
        {
            this.projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.emailSender = emailSender;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult CreateProject()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateProject(CreateProjectInputModel createProjectInputModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var operationResult = await this.projectService.CreateProject(userId, createProjectInputModel);
            if (operationResult.Success)
            {
                this.memoryCache.Set("projetId", operationResult.RelatedObject.Id);

                return this.Redirect($"/ProjectOptions/Overview");
            }
            else
            {
                this.ViewBag.Alert = AlertService.ShowAlert(Alerts.Info, operationResult.InitialException.Message);

                return this.View("CreateProject");
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorOrManagerOrContributorsOrReader)]
        public async Task<IActionResult> MyProjects()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var getOwnerId = await this.projectService.GetOwnerIdByUserId(userId);

            if (getOwnerId.Success)
            {// When owner is not null.
                var opertaionResult = await this.projectService.GetAllProjectByOwnerId(getOwnerId.RelatedObject);

                if (opertaionResult.Success)
                {
                    return this.View(opertaionResult.RelatedObject);
                }
            }
            else if (!getOwnerId.Success)
            {// If its not owner. Is an employee.
                var getEmployeeId = await this.projectService.GetEmployeeIdByUserId(userId);

                var operationResult = await this.projectService.GetAllProjectByEmployeeId(getEmployeeId.RelatedObject);

                if (operationResult.Success)
                {
                    return this.View(operationResult.RelatedObject);
                }
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
