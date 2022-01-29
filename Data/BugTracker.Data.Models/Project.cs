namespace BugTracker.Data.Models
{
    using System.Collections.Generic;

    using BugTracker.Data.Common.Models;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            this.Members = new HashSet<Employee>();
            this.WorkItems = new HashSet<WorkItem>();
        }

        public string OwnerId { get; set; }

        public Owner Owner { get; set; }

        public virtual IEnumerable<Employee> Members { get; set; }

        public virtual IEnumerable<WorkItem> WorkItems { get; set; }
    }
}
