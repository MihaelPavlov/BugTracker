namespace BugTracker.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Westwind.AspNetCore.Markdown;

    public class ProjectOptionsController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly IProjectOptionsService projectOptionsService;
        private readonly IOwnerService ownerService;
        private readonly IMemoryCache memoryCache;
        private IWebHostEnvironment hostEnvironment;

        public ProjectOptionsController(
            IWebHostEnvironment environment,
            IAccountsService accountsService,
            IProjectOptionsService projectOptionsService,
            IOwnerService ownerService,
            IMemoryCache memoryCache)
        {
            this.hostEnvironment = environment ?? throw new ArgumentNullException(nameof(environment));
            this.accountsService = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
            this.projectOptionsService = projectOptionsService ?? throw new ArgumentNullException(nameof(projectOptionsService));
            this.ownerService = ownerService ?? throw new ArgumentNullException(nameof(ownerService));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet]
        public async Task<IActionResult> Overview(string projectId)
        {
            this.SetProjectId(projectId);

            var getMemoryCacheProjectId = this.GetSelectedProjectId();
            if (!getMemoryCacheProjectId.Success)
            {
                return this.Redirect("/Project/MyProjects");
            }

            var model = new OverviewViewModel();

            var operationResult = await this.GetInfo(model, getMemoryCacheProjectId.RelatedObject);

            if (!operationResult.Success)
            {
                model.CompletedWorkItemsCount = 0;
                model.CreatedNoteItemsCount = 0;
                model.WorkItemsCount = 0;
                model.ProjectMembersCount = 0;
            }

            try
            {
                //string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.txt");
                string pathMd = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.md");
                //var txt2 = System.IO.File.ReadAllText(pathTxt);
                var md2 = Markdown.ParseFromUrl(pathMd);

                //model.ReamdeTxt = txt2;
                model.ReamdeMd = md2;
                return this.View(model);
            }
            catch (Exception ex)
            {
                using (FileStream writer = System.IO.File.Create($"wwwroot/{getMemoryCacheProjectId.RelatedObject}-README.txt"))
                {
                }

                using (FileStream writer = System.IO.File.Create($"wwwroot/{getMemoryCacheProjectId.RelatedObject}-README.md"))
                {
                }

                //string newCreatedPathTxt = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.txt");
                string newCreatedPathMd = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.md");

                //string txt = System.IO.File.ReadAllText(newCreatedPathTxt);
                var md = Markdown.ParseFromUrl(newCreatedPathMd);

                //model.ReamdeTxt = txt;
                model.ReamdeMd = md;

                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult EditOverview()
        {
            var getMemoryCacheProjectId = this.GetSelectedProjectId();

            if (!getMemoryCacheProjectId.Success)
            {
                return this.BadRequest();
            }

            string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.txt");

            var readmeTxt = System.IO.File.ReadAllText(pathTxt);

            var model = new EditReadmeViewModel { ReamdeTxt = readmeTxt };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult EditOverview(string textArea)
        {
            var getMemoryCacheProjectId = this.GetSelectedProjectId();

            if (!getMemoryCacheProjectId.Success)
            {
                return this.BadRequest();
            }

            string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.txt");

            string pathMd = Path.Combine(this.hostEnvironment.WebRootPath, $"{getMemoryCacheProjectId.RelatedObject}-README.md");

            using (var writer = System.IO.File.CreateText(pathMd))
            {
                writer.Write(textArea);
                writer.Dispose();
            }

            using (var writer = System.IO.File.CreateText(pathTxt))
            {
                writer.Write(textArea);
                writer.Dispose();
            }

            var model = new OverviewViewModel();
            //string txt = System.IO.File.ReadAllText(pathTxt);
            var md = Markdown.ParseFromUrl(pathMd);

            //model.ReamdeTxt = txt;
            model.ReamdeMd = md;
            return this.View("Overview", model);
        }

        public async Task<IActionResult> WorkItems()
        {
            var getMemoryCacheProjectId = this.GetSelectedProjectId();

            if (!getMemoryCacheProjectId.Success)
            {
                return this.BadRequest();
            }

            var getViewModel = await this.GetWorkItemsViewModel(getMemoryCacheProjectId.RelatedObject);
            return this.View(getViewModel.RelatedObject);
        }

        public async Task<IActionResult> CreateWorkItem(string name , string assignEmployeeEmail)
        {
            var getMemoryCacheProjectId = this.GetSelectedProjectId();

            if (!getMemoryCacheProjectId.Success)
            {
                return this.BadRequest();
            }

            await this.projectOptionsService.CreateWorkItem(getMemoryCacheProjectId.RelatedObject)
            var getViewModel = await this.GetWorkItemsViewModel(getMemoryCacheProjectId.RelatedObject);

            return this.View("WorkItems", getViewModel.RelatedObject);
        }

        public IActionResult ShowTask(string taskId)
        {
            var test = this.memoryCache.Get("projetId");

            object model = taskId;
            return this.View(model);
        }

        public IActionResult Members()
        {
            var test = this.memoryCache.Get("projetId");

            return this.View();
        }

        public IActionResult ProjectNotes()
        {
            return this.View();
        }

        public IActionResult Channels()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMember(string email, MemberStatus status, string role)
        {
            var getProjectId = this.GetSelectedProjectId();

            if (!getProjectId.Success)
            {
                return this.BadRequest();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = await this.ownerService.GetOwnerId(userId);
            var operationResult = await this.accountsService.RegisterEmployee(ownerId, email, getProjectId.RelatedObject, status, role);

            if (!operationResult.Success)
            {
                this.ViewBag.Alert = AlertService.ShowAlert(Alerts.Danger, operationResult.InitialException.Message);
            }

            return this.View("Members");
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(string email, MemberStatus status)
        {
            var getProjectId = this.GetSelectedProjectId();

            if (!getProjectId.Success)
            {
                return this.BadRequest();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = await this.ownerService.GetOwnerId(userId);

            var opertionResult = await this.accountsService.AddEmployee(ownerId, email, getProjectId.RelatedObject, status);

            if (!opertionResult.Success)
            {
                this.ViewBag.Alert = AlertService.ShowAlert(Alerts.Warning, $"Your already have this member in this project! {opertionResult.InitialException.Message}");
            }

            return this.View("Members");
        }

        // Helper methods.

        /// <summary>
        /// Use this method to set project Id in the memory cache.
        /// If project Id is NOT null or empty set the Id.
        /// </summary>
        /// <param name="projectId">A string representing the project Id we want to set.</param>
        public void SetProjectId(string projectId)
        {
            if (!string.IsNullOrEmpty(projectId))
            {
                this.memoryCache.Set("projetId", projectId);
            }
        }

        /// <summary>
        /// Use this method to get string that represent the project Id of the selected project.
        /// If in the memory cache there is a proejctId we will get it.
        /// </summary>
        /// <returns><see cref="OperationResult"/>.</returns>
        public OperationResult<string> GetSelectedProjectId()
        {
            var operationResult = new OperationResult<string>();

            var projectId = this.memoryCache.Get("projetId");

            if (projectId is null)
            {
                operationResult.Success = false;
                return operationResult;
            }

            operationResult.RelatedObject = projectId.ToString();

            return operationResult;
        }

        /// <summary>
        /// Use this method to check is the project selected.
        /// One project is select if in the memory cache there is a proejctId.
        /// </summary>
        /// <returns>A boolean that represent is the project select or not.</returns>
        public bool CheckIsProjectSelected()
        {
            var memoryCacheProjectId = this.memoryCache.Get("projetId");

            if (memoryCacheProjectId is null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="overviewViewModel"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<OperationResult> GetInfo(OverviewViewModel overviewViewModel, string projectId)
        {
            var operationResult = new OperationResult();
            var workItemsCount = await this.projectOptionsService.GetWorkItemsCountByProjectId(projectId);
            var completeWorkItemsCount = await this.projectOptionsService.GetCompletedWorkItemsCountByProjectId(projectId);
            var createdNotesCount = await this.projectOptionsService.GetCreatedNoteItemsCountByProjectId(projectId);
            var membersCount = await this.projectOptionsService.GetProjectMembersCountByProjectId(projectId);

            if (workItemsCount.Success && completeWorkItemsCount.Success && createdNotesCount.Success && membersCount.Success)
            {
                overviewViewModel.WorkItemsCount = workItemsCount.RelatedObject;
                overviewViewModel.CompletedWorkItemsCount = completeWorkItemsCount.RelatedObject;
                overviewViewModel.CreatedNoteItemsCount = createdNotesCount.RelatedObject;
                overviewViewModel.ProjectMembersCount = membersCount.RelatedObject;

                return operationResult;
            }

            operationResult.Success = false;

            return operationResult;
        }

        public async Task<OperationResult<WorkItemsViewModel>> GetWorkItemsViewModel(string projectId)
        {
            var operationResult = new OperationResult<WorkItemsViewModel>();

            try
            {
                var workItems = await this.projectOptionsService.GetAllWorkItemsForProjectByProjectId(projectId);
                var employees = await this.projectOptionsService.GetAllEmployeeByProjectId(projectId);
                var viewModel = new WorkItemsViewModel();
                viewModel.WorkItemsViewModels = workItems.RelatedObject; // get work items
                var workItemsNavabarViewModel = new WorkItemsNavbarViewModel();
                workItemsNavabarViewModel.EmployeeViewModels = employees.RelatedObject;
                viewModel.WorkItemsNavbarViewModel = workItemsNavabarViewModel;

                operationResult.RelatedObject = viewModel;
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }
    }
}
