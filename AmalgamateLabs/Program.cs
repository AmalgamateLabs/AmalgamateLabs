using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace AmalgamateLabs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilderThreePointZero(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilderDefault(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IWebHostBuilder CreateHostBuilderTwoPointOne()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:5000")
                .UseIISIntegration()
                .UseStartup<Startup>();
        }

        private static IHostBuilder CreateHostBuilderThreePointZero(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseUrls("http://localhost:5000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
