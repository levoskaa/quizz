using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace Quizz.Questions;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = CreateSerilogLogger(GetConfiguration());
        var host = CreateHostBuilder(args)
            .Build();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());

    private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        var Namespace = typeof(Startup).Namespace;
        var AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", AppName)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        return builder.Build();
    }
}