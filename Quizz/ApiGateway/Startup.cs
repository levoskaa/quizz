using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //var authenticationProviderKey = "IdentityApiKey";
            //services.AddAuthentication()
            //   .AddJwtBearer(authenticationProviderKey, options =>
            //   {
            //       options.Authority = "http://identity:80";
            //       options.RequireHttpsMetadata = false;
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateAudience = false
            //       };
            //   });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("default");

            app.UseOcelot().Wait();
        }
    }
}