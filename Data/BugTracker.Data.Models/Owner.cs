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
            this.Employees = new HashSet<Employee>();
        }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }
    }
}
