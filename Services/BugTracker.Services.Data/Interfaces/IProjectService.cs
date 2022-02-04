namespace BugTracker.Services.Data.Interfaces
{
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
        Task<OperationResult<string>> GetOwnerIdByUserId(string userId);

        /// <summary>
        /// Use this method to get a collection with all projects that owner have.
        /// </summary>
        /// <param name="ownerId">A string representing the owner Id.</param>
        /// <returns>A collection of <see cref="ProjectViewModel"/>.</returns>
        Task<OperationResult<IEnumerable<ProjectViewModel>>> GetAllProjectByOwnerId(string ownerId);

        /// <summary>
        /// Use this method to get employee Id by user Id.
        /// </summary>
        /// <param name="userId">A string representing the user Id.</param>
        /// <returns>A string representing the Employee Id.</returns>
        Task<OperationResult<string>> GetEmployeeIdByUserId(string userId);

        /// <summary>
        /// Use this method to get a collection with all projects that employee are in.
        /// </summary>
        /// <param name="employeeId">A string representing the employee Id.</param>
        /// <returns>A collection of <see cref="ProjectViewModel"/>.</returns>
        Task<OperationResult<IEnumerable<ProjectViewModel>>> GetAllProjectByEmployeeId(string employeeId);
    }
}
