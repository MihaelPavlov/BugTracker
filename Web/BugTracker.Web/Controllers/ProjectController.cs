namespace BugTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProjectController : Controller
    {
        public IActionResult CreateProject()
        {
            return this.View();
        }

        public IActionResult MyProjects()
        {
            return this.View();
        }

        public IActionResult LatestNews()
        {
            return this.View();
        }
    }
}
