using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PlantListing
{
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        builder.AddSystemsManager("/PlantListing"); // to read configuration from AWS Parameter store e.g. DBUser / DBPassword
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
