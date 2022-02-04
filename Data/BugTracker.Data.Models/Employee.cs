namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>
    {
        public Employee()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Owners = new HashSet<EmployeeOwner>();
            this.ProjectEmployees = new HashSet<ProjectEmployee>();
        }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<EmployeeOwner> Owners { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
