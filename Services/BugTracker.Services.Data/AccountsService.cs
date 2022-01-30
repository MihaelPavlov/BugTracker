namespace BugTracker.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using BugTracker.Data.Common.Repositories;
    using BugTracker.Data.Models;
    using BugTracker.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<Owner> ownerReposiotry;
        private readonly IDeletableEntityRepository<Employee> employeeReposiotry;
        private readonly IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsService(
            IDeletableEntityRepository<Owner> ownerReposiotry,
            IDeletableEntityRepository<Employee> employeeReposiotry,
            IDeletableEntityRepository<EmployeeOwner> employeeOwnerReposiotry,
            UserManager<ApplicationUser> userManager)
        {
            this.ownerReposiotry = ownerReposiotry;
            this.employeeReposiotry = employeeReposiotry;
            this.employeeOwnerReposiotry = employeeOwnerReposiotry;
            this.userManager = userManager;
        }

        public async Task RegisterEmployee(string ownerId, string email, string projectId)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };
            var result = await this.userManager.CreateAsync(applicationUser,"123456");

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

            if (result.Succeeded)
            {
                // TODO: Feature implement Looger
            }
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
    }
}
