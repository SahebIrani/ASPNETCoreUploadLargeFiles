using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
                    .ConfigureKestrel((context, serverOptions) =>
                    {
                        // Handle requests up to 200 MB
                        serverOptions.Limits.MaxRequestBodySize = 209715200;
                        //serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        // Handle requests up to 50 MB
                        //serverOptions.Limits.MaxRequestBodySize = 52428800;

                        serverOptions.Limits.MinRequestBodyDataRate =
                             new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));

                        serverOptions.Limits.MinResponseDataRate =
                            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
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
