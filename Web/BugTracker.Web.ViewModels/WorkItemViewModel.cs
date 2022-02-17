namespace BugTracker.Web.ViewModels
{
    using System;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class WorkItemViewModel : IMapFrom<WorkItem>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string CreateByUserId { get; set; }

        public ApplicationUser CreateByUser { get; set; }

        public string AssignToUserId { get; set; }

        public ApplicationUser AssignToUser { get; set; }

        public DateTime LastActivity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime FinishDate { get; set; }

        public WorkItemStatus Status { get; set; }

        public WorkItemType Type { get; set; }
    }
}
