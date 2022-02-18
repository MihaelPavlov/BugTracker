namespace BugTracker.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class EmployeeViewModel : IMapFrom<Employee>, IMapFrom<Owner>
    {
        public string UserEmail { get; set; }

        public string OwnerEmail { get; set; }
    }
}
