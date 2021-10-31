using BuildingBlocks.HostCustomizations;
using Identity.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    new ConfigurationDbContextSeed()
                        .SeedAsync(context)
                        .Wait();
                });
            //.MigrateDbContext<IdentityContext>((context, services) =>
            //{
            //    var env = services.GetService<IWebHostEnvironment>();
            //    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
            //    var settings = services.GetService<IOptions<AppSettings>>();

            //    new ApplicationDbContextSeed()
            //        .SeedAsync(context, env, logger, settings)
            //        .Wait();
            //});

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