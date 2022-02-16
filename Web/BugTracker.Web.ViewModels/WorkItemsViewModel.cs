namespace BugTracker.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WorkItemsViewModel
    {
        public ICollection<WorkItemViewModel> WorkItemsViewModels { get; set; }

        public WorkItemsNavbarViewModel WorkItemsNavbarViewModel { get; set; }
    }
}
