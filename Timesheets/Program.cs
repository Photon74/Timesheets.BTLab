using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Timesheets.DAL;

namespace Timesheets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!SQLServerConnectionManager.CheckDatabase("timesheets"))
            {
                SQLServerConnectionManager.CreateDatabase();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
