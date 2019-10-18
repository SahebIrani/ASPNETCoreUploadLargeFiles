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
                    //For apps hosted by Kestrel, the default maximum request body size is 30,000,000 bytes, which is approximately 28.6 MB.Customize the limit using the MaxRequestBodySize Kestrel server option:
                    .ConfigureKestrel((context, options) =>
                    {
                        // Handle requests up to 200 MB
                        options.Limits.MaxRequestBodySize = 209715200;
                        //options.Limits.MaxRequestBodySize = 10 * 1024;
                        // Handle requests up to 50 MB
                        //options.Limits.MaxRequestBodySize = 52428800;
                    })
                    .UseKestrel(options =>
                    {
                        // Handle requests up to 50 MB
                        options.Limits.MaxRequestBodySize = 52428800;
                    })
                    .UseStartup<Startup>();
                });
    }
}
