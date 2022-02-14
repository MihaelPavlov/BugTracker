namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BugTracker.Data.Common.Models;

    public class Project : BaseDeletableModel<string>
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Members = new HashSet<ProjectEmployee>();
            this.WorkItems = new HashSet<WorkItem>();
            this.Notes = new HashSet<Note>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        public Owner Owner { get; set; }

        public virtual ICollection<ProjectEmployee> Members { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
