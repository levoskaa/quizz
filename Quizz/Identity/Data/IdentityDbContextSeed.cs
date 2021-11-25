using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{
    public class IdentityDbContextSeed
    {
        public async Task SeedAsync(IdentityDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, "abc123");
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}