namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data.Interfaces;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels.InputModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository;
        private readonly IDeletableEntityRepository<Owner> ownerRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IDeletableEntityRepository<WorkItem> workItemRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ProjectEmployee> projectEmployeeRepository,
            IDeletableEntityRepository<Owner> ownerRepository,
            IDeletableEntityRepository<Employee> employeeRepository,
            IDeletableEntityRepository<WorkItem> workItemRepository,
            IDeletableEntityRepository<ApplicationUser> applicationUserRepository,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.projectEmployeeRepository = projectEmployeeRepository ?? throw new ArgumentNullException(nameof(projectEmployeeRepository));
            this.ownerRepository = ownerRepository ?? throw new ArgumentNullException(nameof(ownerRepository));
            this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this.workItemRepository = workItemRepository ?? throw new ArgumentNullException(nameof(workItemRepository));
            this.applicationUserRepository = applicationUserRepository ?? throw new ArgumentNullException(nameof(applicationUserRepository));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public async Task<OperationResult<Project>> CreateProject(string userId, CreateProjectInputModel createProjectInputModel)
        {
            var operationResult = new OperationResult<Project>();

            try
            {
                var owner = await this.ownerRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
                var user = await this.applicationUserRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
                var project = new Project
                {
                    Name = createProjectInputModel.ProjectName,
                    Description = createProjectInputModel.Description == null ? string.Empty : createProjectInputModel.Description,
                    IsPublic = createProjectInputModel.IsPublic,
                    OwnerId = owner.Id,
                };

                //var apiKey = this.configuration["SendGrid:ApiKey"];
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("rap4obg88@gmail.com", "Example User");
                //var subject = "Sending with SendGrid is Fun";
                //var to = new EmailAddress("m.pavlov1405@gmail.com", "Example User");
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                await this.emailSender.SendEmailAsync("rap4obg88@gmail.com", "BugTracker", "m.pavlov1405@gmail.com", $"New project is created - {project.Name}", "<h1>The new project email!<h1>");

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

        /// <inheritdoc />
        public async Task<OperationResult<IEnumerable<ProjectViewModel>>> GetAllProjectByEmployeeId(string employeeId)
        {
            var operationResult = new OperationResult<IEnumerable<ProjectViewModel>>();

            try
            {
                operationResult.RelatedObject = await this.projectEmployeeRepository
                .All()
                .Where(x => x.EmployeeId == employeeId).Include("Project")
                .Select(x => new ProjectViewModel
                {
                    Id = x.ProjectId,
                    Name = x.Project.Name,
                    OwnerId = x.Project.OwnerId,
                    CompletedTask = x.Project.WorkItems.Count(),
                    Members = x.Project.Members,
                })
                .ToListAsync();

                foreach (var project in operationResult.RelatedObject)
                {
                    project.WorkItems = await this.workItemRepository.All().Where(x => x.ProjectId == project.Id).ToListAsync();
                    var completedWorkItems = await this.workItemRepository.All().Where(x => x.ProjectId == project.Id && x.Status == BugTracker.Data.Enums.WorkItemStatus.Closed).ToListAsync();
                    project.CompletedTask = completedWorkItems.Count();
                }
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<IEnumerable<ProjectViewModel>>> GetAllProjectByOwnerId(string ownerId)
        {
            var operationResult = new OperationResult<IEnumerable<ProjectViewModel>>();

            try
            {
                operationResult.RelatedObject = await this.projectRepository
                    .All()
                    .Where(x => x.OwnerId == ownerId)
                    .To<ProjectViewModel>()
                    .ToListAsync();

                foreach (var project in operationResult.RelatedObject)
                {
                    project.WorkItems = await this.workItemRepository.All().Where(x => x.ProjectId == project.Id).ToListAsync();
                    var completedWorkItems = await this.workItemRepository.All().Where(x => x.ProjectId == project.Id && x.Status == BugTracker.Data.Enums.WorkItemStatus.Closed).ToListAsync();
                    project.CompletedTask = completedWorkItems.Count();
                }
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<string>> GetEmployeeIdByUserId(string userId)
        {
            var operationResult = new OperationResult<string>();

            try
            {
                var employee = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

                operationResult.RelatedObject = employee.Id;

                if (employee == null)
                {
                    operationResult.RelatedObject = string.Empty;
                }
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult<string>> GetOwnerIdByUserId(string userId)
        {
            var operationResult = new OperationResult<string>();

            try
            {
                var owner = await this.ownerRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

                operationResult.RelatedObject = owner.Id;

                if (owner is null)
                {
                    operationResult.Success = false;
                }
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }
    }
}
