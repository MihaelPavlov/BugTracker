namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class ProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public ICollection<Employee> Members { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; }

        public int CompletedTask { get; set; }
    }
}
