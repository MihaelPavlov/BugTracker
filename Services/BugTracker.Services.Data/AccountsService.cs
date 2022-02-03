namespace BugTracker.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Common;
    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Enums;
    using BugTracker.Data.Models;
    using BugTracker.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<Owner> ownerReposiotry;
        private readonly IDeletableEntityRepository<Employee> employeeReposiotry;
        private readonly IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Project> projectReposiotry;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserReposiotry;

        public AccountsService(
            IDeletableEntityRepository<Owner> ownerReposiotry,
            IDeletableEntityRepository<Employee> employeeReposiotry,
            IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Project> projectReposiotry,
            IDeletableEntityRepository<ApplicationUser> applicationUserReposiotry)
        {
            this.ownerReposiotry = ownerReposiotry;
            this.employeeReposiotry = employeeReposiotry;
            this.employeeOwnerReposiotry = employeeOwnerReposiotry;
            this.userManager = userManager;
            this.projectReposiotry = projectReposiotry;
            this.applicationUserReposiotry = applicationUserReposiotry;
        }

        public async Task RegisterEmployee(string ownerId, string email, string projectId, MemberStatus status, string role)
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

            employee.Owners.Add(employeeOwner);

            await this.employeeReposiotry.AddAsync(employee);
            await this.employeeReposiotry.SaveChangesAsync();
            var project = await this.projectReposiotry.All().FirstOrDefaultAsync(x => x.Id == projectId);

            project.Members.Add(employee);
            this.projectReposiotry.Update(project);
            await this.projectReposiotry.SaveChangesAsync();

            if (result.Succeeded)
            {
                // TODO: Feature implement Looger
            }
        }

        public async Task AddEmployee(string ownerId, string email, string projectId, MemberStatus status)
        {
            var applicationUser = await this.applicationUserReposiotry.All().FirstOrDefaultAsync(x => x.Email == email);
            var employee = await this.employeeReposiotry.All().FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);


            var project = await this.projectReposiotry.All().FirstOrDefaultAsync(x => x.Id == projectId);

            project.Members.Add(employee);
            this.projectReposiotry.Update(project);
            await this.projectReposiotry.SaveChangesAsync();
        }

        public async Task RegisterOwner(string userId)
        {
            var owner = new Owner
            {
                UserId = userId,
            };

            await this.ownerReposiotry.AddAsync(owner);
            await this.ownerReposiotry.SaveChangesAsync();
        }

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
