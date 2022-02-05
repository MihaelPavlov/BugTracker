namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Utilities;

    public interface IAccountsService
    {
        /// <summary>
        /// Use this method to register new owner of company.
        /// </summary>
        /// <param name="userId">A string representing the user Id.</param>
        Task<OperationResult> RegisterOwner(string userId);

        /// <summary>
        /// Use this method to register new employee for the project.
        /// </summary>
        /// <param name="ownerId">A string representing the owner Id.</param>
        /// <param name="email">A string representing the email of the user we register.</param>
        /// <param name="projectId">A string representing the project Id to which we are inviting our new employee.</param>
        /// <param name="status">A enum representing the status of the user <see cref="MemberStatus"/>.</param>
        /// <param name="role">A string representing the role of the employee.</param>
        Task<OperationResult> RegisterEmployee(string ownerId, string email, string projectId, MemberStatus status, string role);

        /// <summary>
        /// Use this method to add new employee for project.
        /// </summary>
        /// <param name="ownerId">A string representing the owner Id.</param>
        /// <param name="email">A string representing the email of the user we register.</param>
        /// <param name="projectId">A string representing the project Id to which we are inviting our new employee.</param>
        /// <param name="status">A enum representing the status of the user <see cref="MemberStatus"/>.</param>
        Task<OperationResult> AddEmployee(string ownerId, string email, string projectId, MemberStatus status);
    }
}
