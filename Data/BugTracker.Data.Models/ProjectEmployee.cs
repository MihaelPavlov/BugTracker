namespace BugTracker.Data.Models
{
    using System;

    using BugTracker.Data.Common.Models;

    public class ProjectEmployee : IDeletableEntity
    {
        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
