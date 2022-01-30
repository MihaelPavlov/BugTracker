namespace BugTracker.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAccountsService
    {
        Task RegisterOwner(string userId);

        Task RegisterEmployee(string ownerId,string email, string projectId);
    }
}
