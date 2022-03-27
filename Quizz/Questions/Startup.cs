using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Questions.Infrastructure.Mapper;
using Quizz.Questions.Data;
using Quizz.Questions.Infrastructure;
using Quizz.Questions.Infrastructure.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Quizz.Questions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new QuestionsModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // TODO
            // services.AddMediatR(typeof(CreateGameCommandHandler).Assembly);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Questions", Version = "v1" });
            });

            services.AddDbContext<QuestionsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(QuestionsContext).Assembly.GetName().Name);
                    });
            });

            // Don't override JWT claim names
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var idserverCertPem = File.ReadAllText("Certificates/idserver_signed_cert.pem");
            var idserverKeyPem = File.ReadAllText("Certificates/idserver_private_key.pem");
            var idserverCert = X509Certificate2.CreateFromPem(idserverCertPem, idserverKeyPem);
            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = Configuration["Identity:Authority"];
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false,
                       IssuerSigningKey = new X509SecurityKey(idserverCert),
                   };
               });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Questions v1"));
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}