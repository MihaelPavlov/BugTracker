namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using BugTracker.Data.Utilities;

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
    }
}
