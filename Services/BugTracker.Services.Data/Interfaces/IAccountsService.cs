namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;
    using BugTracker.Data.Utilities;

    public interface IAccountsService
    {
        Task<OperationResult> RegisterOwner(string userId);

        Task<OperationResult> RegisterEmployee(string ownerId, string email, string projectId, MemberStatus status, string role);

        Task<OperationResult> AddEmployee(string ownerId, string email, string projectId, MemberStatus status);
    }
}
