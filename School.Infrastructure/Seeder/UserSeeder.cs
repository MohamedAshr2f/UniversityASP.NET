using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;

namespace School.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public async static Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "UniversityProject",
                    Country = "Egypt",
                    PhoneNumber = "123456",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "Admin@@2025");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}
