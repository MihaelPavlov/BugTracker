namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.InputModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.EntityFrameworkCore;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<Owner> ownerRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<Owner> ownerRepository,
            IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.projectRepository = projectRepository;
            this.ownerRepository = ownerRepository;
            this.employeeRepository = employeeRepository;
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

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectByEmployeeId(string employeeId)
        {
            var projects = await this.projectRepository.All().ToListAsync();

            var te = projects.Where(x => x.Members.Where(y => y.Id == employeeId).Any()).Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Name = x.Name,
                OwnerId = x.OwnerId,
                CompletedTask = 10,
                WorkItems = new HashSet<WorkItem>()
            }).ToList();

            return te;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectByOwnerId(string ownerId)
        {
            var projectsByOwnerId = await this.projectRepository.All().Where(x => x.OwnerId == ownerId).To<ProjectViewModel>().ToListAsync();

            return projectsByOwnerId;
        }

        public async Task<string> GetEmployeeIdByUserId(string userId)
        {
            var employee = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

            if (employee == null)
            {
                return string.Empty;
            }

            return employee.Id;
        }

        public async Task<string> GetOwnerIdByUserId(string userId)
        {
            var owner = await this.ownerRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

            if (owner == null)
            {
                return string.Empty;
            }

            return owner.Id;
        }
    }
}
