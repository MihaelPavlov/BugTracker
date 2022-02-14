namespace BugTracker.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OverviewViewModel
    {
        public string ReamdeMd { get; set; }

        public int WorkItemsCount { get; set; }

        public int CompletedWorkItemsCount { get; set; }

        public int CreatedNoteItemsCount { get; set; }

        public int ProjectMembersCount { get; set; }
    }
}
