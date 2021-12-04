using BuildingBlocks.HostCustomizations;
using Identity.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug);
                    builder.AddFilter("IdentityServer4", LogLevel.Debug);
                })
                .Build();

            host.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    new ConfigurationDbContextSeed()
                        .SeedAsync(context)
                        .Wait();
                })
                .MigrateDbContext<IdentityDbContext>((context, services) =>
                {
                    new IdentityDbContextSeed()
                        .SeedAsync(context)
                        .Wait();
                });

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}