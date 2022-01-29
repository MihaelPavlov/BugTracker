namespace BugTracker.Data.Models
{
    using System;

    using BugTracker.Data.Common.Models;

    public class WorkItem : BaseDeletableModel<string>
    {
        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string CreateByEmployeeId { get; set; }

        public Employee CreateByEmployee { get; set; }

        public string AssignToEmployeeId { get; set; }

        public Employee AssignToEmployee { get; set; }

        public string Name { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
