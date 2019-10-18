
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Simple
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            //using (ServerManager serverManager = new ServerManager())
            //{
            //    Configuration config = serverManager.GetWebConfiguration("Default Web Site");
            //    ConfigurationSection requestFilteringSection = config.GetSection("system.webServer/security/requestFiltering");
            //    ConfigurationElement requestLimitsElement = requestFilteringSection.GetChildElement("requestLimits");
            //    ConfigurationElementCollection headerLimitsCollection = requestLimitsElement.GetCollection("headerLimits");

            //    ConfigurationElement addElement = headerLimitsCollection.CreateElement("add");
            //    addElement["header"] = @"Content-type";
            //    addElement["sizeLimit"] = 100;
            //    headerLimitsCollection.Add(addElement);

            //    serverManager.CommitChanges();
            //}

            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //For apps hosted by Kestrel, the default maximum request body size is 30,000,000 bytes, which is approximately 28.6 MB.Customize the limit using the MaxRequestBodySize Kestrel server option:
                    .ConfigureKestrel((context, serverOptions) =>
                    {
                        //Set Upload Timeout
                        //serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(120);
                        //serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(120);

                        //serverOptions.Limits.MaxRequestBodySize = 500_000_000;
                        //serverOptions.Limits.MaxResponseBufferSize = null;
                        //serverOptions.Limits.MaxResponseBufferSize = null;

                        // Handle requests up to 200 MB
                        //serverOptions.Limits.MaxRequestBodySize = 209715200;
                        //serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        // Handle requests up to 50 MB
                        //serverOptions.Limits.MaxRequestBodySize = 52428800;

                        // Handle requests up to 4 GB 4294967296
                        //InvalidDataException: Multipart body length limit 134217728 exceeded.
                        serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                        serverOptions.Limits.MaxResponseBufferSize = long.MaxValue;
                        serverOptions.Limits.MaxResponseBufferSize = long.MaxValue;

                        //serverOptions.Limits.MinRequestBodyDataRate =
                        //     new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));

                        //serverOptions.Limits.MinResponseDataRate =
                        //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                    })
                    //.UseKestrel(options =>
                    //{
                    //    // Handle requests up to 50 MB
                    //    options.Limits.MaxRequestBodySize = 52428800;
                    //})
                    .UseHttpSys(options =>
                    {
                        //options.MaxRequestBodySize = 100_000_000;
                    })
                    .UseStartup<Startup>();
                });
    }
}