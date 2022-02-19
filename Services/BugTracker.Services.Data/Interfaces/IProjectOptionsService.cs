namespace BugTracker.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Utilities;
    using BugTracker.Web.ViewModels;

    public interface IProjectOptionsService
    {
        /// <summary>
        /// Use this method to get the count of all work items for a particular project by project Id.
        /// </summary>
        /// <param name="projectId">A string representing the project Id.</param>
        /// <returns>A integer represent the count of the work items for a particular project.</returns>
        Task<OperationResult<int>> GetWorkItemsCountByProjectId(string projectId);

        /// <summary>
        /// Use this method to get the count of all completed work items for a particular project by project Id.
        /// </summary>
        /// <param name="proejctId">A string representing the project Id.</param>
        /// <returns>A integer represent the count of the completed work items for a particular project.</returns>
        Task<OperationResult<int>> GetCompletedWorkItemsCountByProjectId(string projectId);

        /// <summary>
        /// Use this method to get the count of all created note items for a particular project by project Id.
        /// </summary>
        /// <param name="proejctId">A string representing the project Id.</param>
        /// <returns>A integer represent the count of the created note items for a particular project.</returns>
        Task<OperationResult<int>> GetCreatedNoteItemsCountByProjectId(string projectId);

        /// <summary>
        /// Use this method to get the count of all members for a particular project by project Id.
        /// </summary>
        /// <param name="projectId">A string representing the project Id.</param>
        /// <returns>A integer represent the count of the members for a particular project.</returns>
        Task<OperationResult<int>> GetProjectMembersCountByProjectId(string projectId);

        /// <summary>
        /// Use this method to get all employees in project by projectId.
        /// </summary>
        /// <param name="projectId">A string representing the project Id.</param>
        /// <returns>A collection of viewModels <see cref="EmployeeViewModel"/>.</returns>
        Task<OperationResult<ICollection<EmployeeViewModel>>> GetAllEmployeeByProjectId(string projectId);

        /// <summary>
        /// Use this method to get all work items for particular project by projectId.
        /// </summary>
        /// <param name="projectId">A string representing the project Id.</param>
        /// <returns>A collection of viewModels <see cref="WorkItemViewModel"/>.</returns>
        Task<OperationResult<ICollection<WorkItemViewModel>>> GetAllWorkItemsForProjectByProjectId(string projectId);

        /// <summary>
        /// Use this method to create new work item.
        /// </summary>
        /// <param name="projectId">A string representing the project Id.</param>
        /// <param name="name">A string representing the work item name.</param>
        /// <param name="createByUserId">A string representing the user Id who create the work item.</param>
        /// <param name="assignToUserEmail">A string representing the email of the user who get the work item.</param>
        /// <param name="type">A enum representing the type of the work item.</param>
        /// <returns>A string representing the Id of the new created work item.</returns>
        Task<OperationResult<string>> CreateWorkItem(string projectId, string name, string createByUserId, string assignToUserEmail, WorkItemType type);
    }
}
