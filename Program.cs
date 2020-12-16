using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;


namespace TestFeatureFlags
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    var connection = settings.GetConnectionString("AppConfig");
                    var env = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddAzureAppConfiguration(options =>
                                        options
                                        .Connect(connection)
                                        .UseFeatureFlags(opt =>
                                        {
                    opt.Label = "Other";
                    opt.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                }));
                }).UseStartup<Startup>());
        }
    }
}
