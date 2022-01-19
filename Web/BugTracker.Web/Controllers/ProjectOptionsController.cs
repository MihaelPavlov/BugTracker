﻿namespace BugTracker.Web.Controllers
{
    using System.IO;
    using BugTracker.Web.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Westwind.AspNetCore.Markdown;

    public class ProjectOptionsController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;

        public ProjectOptionsController(IWebHostEnvironment environment)
        {
            this._hostEnvironment = environment;
        }

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

        public IActionResult MarkdownPageTemplate()
        {
            var model = new ReadmeViewModel();

            try
            {
                string pathTxt = Path.Combine(this._hostEnvironment.WebRootPath, "README.txt");
                string pathMd = Path.Combine(this._hostEnvironment.WebRootPath, "README.md");
                var txt2 = System.IO.File.ReadAllText(pathTxt);
                var md2 = Markdown.ParseFromUrl(pathMd);

                model.ReamdeTxt = txt2;
                model.ReamdeMd = md2;
                return this.View(model);
            }
            catch (System.Exception ex)
            {

                using (FileStream writer = System.IO.File.Create("wwwroot/README.txt"))
                {
                }

                string newCreatedPathTxt = Path.Combine(this._hostEnvironment.WebRootPath, "README.txt");
                string newCreatedPathMd = Path.Combine(this._hostEnvironment.WebRootPath, "README.md");

                string txt = System.IO.File.ReadAllText(newCreatedPathTxt);
                var md = Markdown.ParseFromUrl(newCreatedPathMd);

                model.ReamdeTxt = txt;
                model.ReamdeMd = md;

                return this.View(model);
            }

        }

        [HttpPost]
        public IActionResult MarkdownPageTemplate(string textArea)
        {
            string pathTxt = Path.Combine(this._hostEnvironment.WebRootPath, "README.txt");

            string pathMd = Path.Combine(this._hostEnvironment.WebRootPath, "README.md");

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
            return this.View("MarkdownPageTemplate", model);
        }
    }
}
