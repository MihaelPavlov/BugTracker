namespace BugTracker.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;

    public class CreateWorkItemInputModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Project Name should be max 15 characters.")]
        [MinLength(3, ErrorMessage = "Project Name should be min 3 characters.")]
        public string Name { get; set; }

        [Required]
        public WorkItemStatus Status { get; set; }

        public Employee AssignToEmployee { get; set; }
    }
}
