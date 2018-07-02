using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GraphQL.Linq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string [] args)
        {
            var directory = Directory.GetCurrentDirectory();

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(directory)
                .UseIISIntegration()
                .UseStartup<Startup>();
        }
    }
}
