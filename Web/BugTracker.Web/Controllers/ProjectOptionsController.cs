namespace BugTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProjectOptionsController : Controller
    {
        public IActionResult AllOpenTasks()
        {
            return this.View();
        }

        public IActionResult ShowTask(string taskId)
        {
            object model = taskId;
            return this.View(model);
        }

        public IActionResult Members()
        {
            return this.View();
        }

        public IActionResult ProjectNotes()
        {
            return this.View();
        }
    }
}
