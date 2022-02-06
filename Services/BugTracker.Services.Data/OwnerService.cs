namespace BugTracker.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class OwnerService : IOwnerService
    {
        private readonly IDeletableEntityRepository<Owner> ownerRepository;

        public OwnerService(IDeletableEntityRepository<Owner> ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        public async Task<string> GetOwnerId(string userId)
        {
            var result = await this.ownerRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return result.Id;
        }
    }
}
