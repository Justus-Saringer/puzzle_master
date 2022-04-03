using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PuzzleMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseUtil.createDatabaseIfNotExist();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                        .UseUrls("http://0.0.0.0:5000", "http://0.0.0.0:5001")
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }
}
