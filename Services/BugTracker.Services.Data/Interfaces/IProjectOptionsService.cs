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
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<OperationResult> CreateWorkItem(string projectId, string name, string createByUserId, string assignToUserEmail, WorkItemType type, WorkItemStatus status = WorkItemStatus.New);
    }
}
