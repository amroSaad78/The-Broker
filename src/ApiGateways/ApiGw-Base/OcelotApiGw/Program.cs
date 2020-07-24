using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Serilog;

namespace OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(s => s.AddSingleton(webBuilder))
                    .ConfigureAppConfiguration(ic => ic.AddJsonFile(Path.Combine("configuration", "configuration.json")))
                    .UseStartup<Startup>()
                    .ConfigureLogging((hostingContext, loggingbuilder) =>
                    {
                        loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        loggingbuilder.AddConsole();
                        loggingbuilder.AddDebug();
                    })
                    .UseSerilog((builderContext, config) =>
                    {
                        config
                            .MinimumLevel.Information()
                            .Enrich.FromLogContext()
                            .WriteTo.Console();
                    });
                });
    }
}
