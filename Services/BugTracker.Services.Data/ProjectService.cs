namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.InputModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.EntityFrameworkCore;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository;
        private readonly IDeletableEntityRepository<Owner> ownerRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository,
            IDeletableEntityRepository<Owner> ownerRepository,
            IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.projectEmployeeRepository = projectEmployeeRepository ?? throw new ArgumentNullException(nameof(projectEmployeeRepository));
            this.ownerRepository = ownerRepository ?? throw new ArgumentNullException(nameof(ownerRepository));
            this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<OperationResult<Project>> CreateProject(string userId, CreateProjectInputModel createProjectInputModel)
        {
            var operationResult = new OperationResult<Project>();

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

                operationResult.RelatedObject = project;
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectByEmployeeId(string employeeId)
        {
            var allProjectsByEmployee = await this.projectEmployeeRepository
                .All()
                .Where(x => x.EmployeeId == employeeId).Include("Project")
                .Select(x => new ProjectViewModel
                {
                    Id = x.ProjectId,
                    Name = x.Project.Name,
                    OwnerId = x.Project.OwnerId,
                    CompletedTask = x.Project.WorkItems.Count(),
                    Members = x.Project.Members,
                    WorkItems = x.Project.WorkItems,
                })
                .ToListAsync();

            return allProjectsByEmployee;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectByOwnerId(string ownerId)
        {
            var projectsByOwnerId = await this.projectRepository
                .All()
                .Where(x => x.OwnerId == ownerId)
                .To<ProjectViewModel>()
                .ToListAsync();

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
