namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Common.Models;

    public class Owner : BaseDeletableModel<string>
    {
        public Owner()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Employees = new HashSet<EmployeeOwner>();
        }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<EmployeeOwner> Employees { get; set; }
    }
}
