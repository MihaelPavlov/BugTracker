namespace BugTracker.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOwnerService
    {
        /// <summary>
        /// Use this method to get owner id by application user Id.
        /// </summary>
        /// <param name="userId">A string representing the application user Id.</param>
        /// <returns>A string representing the owner Id.</returns>
        Task<string> GetOwnerId(string userId);
    }
}
