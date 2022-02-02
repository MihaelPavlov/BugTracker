namespace BugTracker.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using BugTracker.Data.Enums;

    public interface IAccountsService
    {
        Task RegisterOwner(string userId);

        Task RegisterEmployee(string ownerId, string email, string projectId, MemberStatus status, string role);
    }
}
