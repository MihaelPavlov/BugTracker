namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Web.ViewModels.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<Owner> ownerRepository;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<Owner> ownerRepository)
        {
            this.projectRepository = projectRepository;
            this.ownerRepository = ownerRepository;
        }

        public async Task<bool> CreateProject(string userId, CreateProjectInputModel createProjectInputModel)
        {
            try
            {
                var owner = await this.ownerRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
                var project = new Project
                {
                    Name = createProjectInputModel.ProjectName,
                    OwnerId = owner.Id,
                };

                await this.projectRepository.AddAsync(project);
                await this.projectRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
