using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quizz.Identity.Data;
using Quizz.Identity.Models;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Quizz.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity", Version = "v1" });
            });

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            var certPem = File.ReadAllText("Certificates/idserver_signed_cert.pem");
            var keyPem = File.ReadAllText("Certificates/idserver_private_key.pem");
            var cert = X509Certificate2.CreateFromPem(certPem, keyPem);
            services.AddIdentityServer(options =>
                {
                    options.IssuerUri = Configuration["Identity:Issuer"];
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString,
                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString,
                            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddSigningCredential(cert);

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "204975450703-tnos0oce0537bc8lcqfubbs4f3kt9d9a.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}