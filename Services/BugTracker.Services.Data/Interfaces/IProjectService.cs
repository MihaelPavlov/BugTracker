namespace BugTracker.Services.Data.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Web.ViewModels.InputModels;
    using BugTracker.Web.ViewModels.Projects;

    public interface IProjectService
    {
        /// <summary>
        /// Use this method to create project.
        /// </summary>
        /// <param name="userId">A string representing the user Id.</param>
        /// <param name="createProjectInputModel">Input model <see cref="CreateProjectInputModel"/>.</param>
        Task<OperationResult<Project>> CreateProject(string userId, CreateProjectInputModel createProjectInputModel);

        /// <summary>
        /// Use this method to get the owner Id from user Id.
        /// </summary>
        /// <param name="userId">A string representing the user Id.</param>
        /// <returns>A string representing the owner Id.</returns>
        Task<string> GetOwnerIdByUserId(string userId);

        /// <summary>
        /// Use this method to get collection with all projects that owner have.
        /// </summary>
        /// <param name="ownerId">A string representing the owner Id.</param>
        /// <returns>A collection of <see cref="ProjectViewModel"/>.</returns>
        Task<IEnumerable<ProjectViewModel>> GetAllProjectByOwnerId(string ownerId);

        Task<string> GetEmployeeIdByUserId(string userId);

        Task<IEnumerable<ProjectViewModel>> GetAllProjectByEmployeeId(string employeeId);
    }
}
