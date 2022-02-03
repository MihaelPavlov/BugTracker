namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.InputModels;

    public interface IProjectService
    {
        Task<bool> CreateProject(string userId, CreateProjectInputModel createProjectInputModel);
    }
}
