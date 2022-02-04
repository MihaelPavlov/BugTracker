namespace BugTracker.Web.Controllers
{
    using System;
    using System.IO;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels;
    using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Westwind.AspNetCore.Markdown;

    public class ProjectOptionsController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly IOwnerService ownerService;
        private readonly IMemoryCache memoryCache;
        private IWebHostEnvironment hostEnvironment;

        public ProjectOptionsController(
            IWebHostEnvironment environment,
            IAccountsService accountsService,
            IOwnerService ownerService,
            IMemoryCache memoryCache)
        {
            this.hostEnvironment = environment;
            this.accountsService = accountsService;
            this.ownerService = ownerService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Overview(string projectId)
        {
            if (!string.IsNullOrEmpty(projectId))
            {
                this.memoryCache.Set("projetId", projectId);
            }

            var memoryCacheProjectId = this.memoryCache.Get("projetId");
            if (memoryCacheProjectId == null)
            {
                return this.Redirect("/Project/MyProjects");
            }

            var model = new ReadmeViewModel();

            try
            {
                string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, "README.txt");
                string pathMd = Path.Combine(this.hostEnvironment.WebRootPath, "README.md");
                var txt2 = System.IO.File.ReadAllText(pathTxt);
                var md2 = Markdown.ParseFromUrl(pathMd);

                model.ReamdeTxt = txt2;
                model.ReamdeMd = md2;
                return this.View(model);
            }
            catch (Exception ex)
            {

                using (FileStream writer = System.IO.File.Create("wwwroot/README.txt"))
                {
                }

                string newCreatedPathTxt = Path.Combine(this.hostEnvironment.WebRootPath, "README.txt");
                string newCreatedPathMd = Path.Combine(this.hostEnvironment.WebRootPath, "README.md");

                string txt = System.IO.File.ReadAllText(newCreatedPathTxt);
                var md = Markdown.ParseFromUrl(newCreatedPathMd);

                model.ReamdeTxt = txt;
                model.ReamdeMd = md;

                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult EditOverview()
        {
            var model = new ReadmeViewModel();

            string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, "README.txt");
            string pathMd = Path.Combine(this.hostEnvironment.WebRootPath, "README.md");
            var txt2 = System.IO.File.ReadAllText(pathTxt);
            var md2 = Markdown.ParseFromUrl(pathMd);

            model.ReamdeTxt = txt2;
            model.ReamdeMd = md2;
            return this.View(model);
        }

        public IActionResult WorkItems()
        {
            var test = this.memoryCache.Get("projetId");

            return this.View();
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
            var projectId = this.memoryCache.Get("projetId").ToString();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = await this.ownerService.GetOwnerId(userId);
            await this.accountsService.RegisterEmployee(ownerId, email, projectId, status, role);

            return this.RedirectToAction("WorkItems");
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(string email, MemberStatus status)
        {
            var projectId = this.memoryCache.Get("projetId").ToString();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = await this.ownerService.GetOwnerId(userId);

            try
            {
                await this.accountsService.AddEmployee(ownerId, email, projectId, status);
            }
            catch (Exception ex)
            {
                this.ViewBag.Alert = CommonService.ShowAlert(Alerts.Warning, "Your already have this member in this project!");
            }

            return this.View("Members");
        }

        [HttpPost]
        public IActionResult EditOverview(string textArea)
        {
            string pathTxt = Path.Combine(this.hostEnvironment.WebRootPath, "README.txt");

            string pathMd = Path.Combine(this.hostEnvironment.WebRootPath, "README.md");

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

            var model = new ReadmeViewModel();
            string txt = System.IO.File.ReadAllText(pathTxt);
            var md = Markdown.ParseFromUrl(pathMd);

            model.ReamdeTxt = txt;
            model.ReamdeMd = md;
            return this.View("Overview", model);
        }
    }
}
