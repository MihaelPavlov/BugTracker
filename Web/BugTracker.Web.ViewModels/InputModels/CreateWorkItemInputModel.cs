namespace BugTracker.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Web.Infrastructure;

    public class CreateWorkItemInputModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Project Name should be max 15 characters.")]
        [MinLength(3, ErrorMessage = "Project Name should be min 3 characters.")]
        public string Name { get; set; }

        [RequiredEnumFieldAttribute(ErrorMessage = "Type is required!")]
        public WorkItemType Type { get; set; }

        public string UserEmail { get; set; }
    }
}
