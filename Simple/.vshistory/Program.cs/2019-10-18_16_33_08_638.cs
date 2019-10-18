using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Simple
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel((context, options) =>
                    {
                        // Handle requests up to 200 MB
                        options.Limits.MaxRequestBodySize = 209715200;
                        // Handle requests up to 50 MB
                        //options.Limits.MaxRequestBodySize = 52428800;
                    })
                    .UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = 209715200;
                    })
                    .UseStartup<Startup>();
                });
    }
}
