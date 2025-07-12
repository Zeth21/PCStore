using Microsoft.AspNetCore.Identity;
using PCStore.Domain.Entities;

namespace PCStore.Application.RoleControl
{
    public interface ISeedData
    {
        Task SeedRolesAsync(RoleManager<IdentityRole> roleManager);
        Task SeedUsersAsync(UserManager<User> userManager);
    }
}
