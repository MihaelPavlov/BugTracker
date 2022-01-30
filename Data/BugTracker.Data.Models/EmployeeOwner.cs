namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BugTracker.Data.Common.Models;

    public class EmployeeOwner : IDeletableEntity
    {
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string OwnerId { get; set; }

        public Owner Owner { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
