namespace BugTracker.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Common;
    using BugTracker.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.ManagerRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.ContributorsRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.ReaderRoleName);

        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
