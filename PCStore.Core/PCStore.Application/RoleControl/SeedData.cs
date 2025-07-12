using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCStore.Domain.Entities;

namespace PCStore.Application.RoleControl
{
    public class SeedData : ISeedData
    {

        async Task ISeedData.SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Customer" };
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        async Task ISeedData.SeedUsersAsync(UserManager<User> userManager)
        {
            var adminUser = new User
            {
                Name = "Admin",
                Surname = "Admin",
                Email = "admin@example.com",
                UserName = "Admin",
                EmailConfirmed = true
            };
            var customerUser = new User
            {
                Name = "Customer",
                Surname = "Customer",
                Email = "customer@example.com",
                UserName = "Customer",
                EmailConfirmed = true
            };
            async Task CreateUser(User user)
            {
                var role = "Customer";
                if (user.UserName == "Admin") { role = "Admin"; }
                var checkUser = await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                if (checkUser is null)
                {
                    var result = await userManager.CreateAsync(user, "Test1234!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new InvalidOperationException(errors);
                    }
                }
                else
                {
                    var checkRole = await userManager.IsInRoleAsync(checkUser, role);
                    if (checkRole is false)
                    {
                        var roleResult = await userManager.AddToRoleAsync(checkUser, role);
                        if (!roleResult.Succeeded)
                        {
                            string errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                            throw new InvalidOperationException(errors);
                        }
                    }
                }
            }

            await CreateUser(adminUser);
            await Task.Delay(100);
            await CreateUser(customerUser);

        }
    }
}
