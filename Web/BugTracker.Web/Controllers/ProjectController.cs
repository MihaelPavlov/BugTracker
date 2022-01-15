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

        public IActionResult ProjectView()
        {
            return this.View();
        }

        public IActionResult ShowTask(string taskId)
        {
            object model = taskId;
            return this.View(model);
        }
    }
}
