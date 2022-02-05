namespace BugTracker.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Data.Utilities;
    using BugTracker.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<Owner> ownerReposiotry;
        private readonly IDeletableEntityRepository<Employee> employeeReposiotry;
        private readonly IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry;
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserReposiotry;
        private readonly IDeletableEntityRepository<ProjectEmployee> projectEmployeeReposiotry;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsService(
            IDeletableEntityRepository<Owner> ownerReposiotry,
            IDeletableEntityRepository<Employee> employeeReposiotry,
            IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry,
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ApplicationUser> applicationUserReposiotry,
            IDeletableEntityRepository<ProjectEmployee> projectEmployeeReposiotry,
            UserManager<ApplicationUser> userManager)
        {
            this.ownerReposiotry = ownerReposiotry ?? throw new ArgumentNullException(nameof(ownerReposiotry));
            this.employeeReposiotry = employeeReposiotry ?? throw new ArgumentNullException(nameof(employeeReposiotry));
            this.employeeOwnerReposiotry = employeeOwnerReposiotry ?? throw new ArgumentNullException(nameof(employeeOwnerReposiotry));
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.applicationUserReposiotry = applicationUserReposiotry ?? throw new ArgumentNullException(nameof(applicationUserReposiotry));
            this.projectEmployeeReposiotry = projectEmployeeReposiotry ?? throw new ArgumentNullException(nameof(projectEmployeeReposiotry));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc />
        public async Task<OperationResult> RegisterEmployee(string ownerId, string email, string projectId, MemberStatus status, string role)
        {
            var operationResult = new OperationResult();

            try
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                };

                var result = await this.userManager.CreateAsync(applicationUser, this.GeneratePassword(10));
                await this.userManager.AddToRoleAsync(applicationUser, role);

                var employee = new Employee
                {
                    UserId = applicationUser.Id,
                    User = applicationUser,
                };

                var employeeOwner = new EmployeeOwner
                {
                    EmployeeId = employee.Id,
                    OwnerId = ownerId,
                };

                var projectEmployee = new ProjectEmployee
                {
                    EmployeeId = employee.Id,
                    ProjectId = projectId,
                };

                await this.employeeReposiotry.AddAsync(employee);
                await this.employeeReposiotry.SaveChangesAsync();

                await this.employeeOwnerReposiotry.AddAsync(employeeOwner);
                await this.employeeOwnerReposiotry.SaveChangesAsync();

                await this.projectEmployeeReposiotry.AddAsync(projectEmployee);
                await this.projectEmployeeReposiotry.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult> AddEmployee(string ownerId, string email, string projectId, MemberStatus status)
        {
            var operationResult = new OperationResult();

            try
            {
                var applicationUser = await this.applicationUserReposiotry.All().FirstOrDefaultAsync(x => x.Email == email);

                var employee = await this.employeeReposiotry.All().FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);

                var isEmployeeAlreadyWorkForThisOwner = await this.employeeOwnerReposiotry.All().AnyAsync(x => x.OwnerId == ownerId && x.EmployeeId == employee.Id);

                // If this employee is new for this owner. Then create it.
                if (!isEmployeeAlreadyWorkForThisOwner)
                {
                    var employeeOwner = new EmployeeOwner
                    {
                        EmployeeId = employee.Id,
                        OwnerId = ownerId,
                    };

                    await this.employeeOwnerReposiotry.AddAsync(employeeOwner);
                    await this.employeeOwnerReposiotry.SaveChangesAsync();
                }

                var projectEmployee = new ProjectEmployee
                {
                    ProjectId = projectId,
                    EmployeeId = employee.Id,
                    Employee = employee,
                };

                await this.projectEmployeeReposiotry.AddAsync(projectEmployee);
                await this.projectEmployeeReposiotry.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        /// <inheritdoc />
        public async Task<OperationResult> RegisterOwner(string userId)
        {
            var operationResult = new OperationResult();

            try
            {
                var owner = new Owner
                {
                    UserId = userId,
                };

                await this.ownerReposiotry.AddAsync(owner);
                await this.ownerReposiotry.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.AppendError(ex);
            }

            return operationResult;
        }

        // Helper methods.
        private string GeneratePassword(int length)
        {
            string password = Guid.NewGuid().ToString();
            password = password.Replace("-", string.Empty);

            if (length <= 0 || length > password.Length)
            {
                throw new ArgumentException("Length must be between 1 and " + password.Length);
            }

            return password.Substring(0, length);
        }
    }
}
