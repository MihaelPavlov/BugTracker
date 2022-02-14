namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BugTracker.Data.Common.Models;
    using BugTracker.Data.Enums;

    public class Activity : BaseDeletableModel<string>
    {
        public Activity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Message { get; set; }

        public ProjectActivityTypes ProjectActivityType { get; set; }
    }
}
