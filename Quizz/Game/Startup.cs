using Autofac;
using MediatR;
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
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Data;
using Quizz.GameService.Infrastructure;
using Quizz.GameService.Infrastructure.Exceptions;
using Quizz.GameService.Infrastructure.Mapper;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Quizz.GameService;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new GameServiceModule());
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddMediatR(typeof(CreateGameCommandHandler).Assembly);

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        services.AddHttpContextAccessor();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameService", Version = "v1" });
        });

        services.AddDbContext<GameContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("Default"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(GameContext).Assembly.GetName().Name);
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

        // TODO: fix authorization
        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("ApiScope", policy =>
        //    {
        //        policy.RequireAuthenticatedUser();
        //        policy.RequireClaim("scope", "game");
        //    });
        //});

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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game v1"));
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseRouting();

        app.UseCors("default");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // TODO: .RequireAuthorization("ApiScope");
        });
    }
}
