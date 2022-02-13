namespace BugTracker.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProjectInputModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Project Name should be max 15 characters.")]
        [MinLength(3, ErrorMessage = "Project Name should be min 3 characters.")]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; } = true;
    }
}
