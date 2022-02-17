namespace BugTracker.Data.Models
{
    using System;

    using BugTracker.Data.Common.Models;
    using BugTracker.Data.Enums;

    public class WorkItem : BaseDeletableModel<string>
    {
        public WorkItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string CreateByUserId { get; set; }

        public ApplicationUser CreateByUser { get; set; }

        public string AssignToUserId { get; set; }

        public ApplicationUser AssignToUser { get; set; }

        public DateTime LastActivity { get; set; }

        public DateTime FinishDate { get; set; }

        public WorkItemStatus Status { get; set; }

        public WorkItemType Type { get; set; }
    }
}
