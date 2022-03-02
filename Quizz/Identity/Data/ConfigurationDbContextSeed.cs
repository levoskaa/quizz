using Quizz.Identity.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Quizz.Identity.Data
{
    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var api in Config.ApiScopes)
                {
                    context.ApiScopes.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }
    }
}