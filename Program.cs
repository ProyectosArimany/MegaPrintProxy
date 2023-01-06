using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

// publicar con: dotnet publish -r linux-x64 --self-contained false --configuration Release
namespace MegaPrintProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(serverOptions => {
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                        serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        serverOptions.Limits.MaxRequestBodySize = null; //30 * 1024
                        serverOptions.Limits.MinRequestBodyDataRate = null; //new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        serverOptions.Limits.MinResponseDataRate = null; // new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                       serverOptions.Listen(IPAddress.Any, 8080);
    //                    serverOptions.Listen(IPAddress.Loopback, 5003, listenOptions =>
    //                                            {
    //                                                listenOptions.UseHttps("testCert.pfx", 
    //                                                    "testPassword");
    //                                            });
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                        serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                    });
                });
    }
}
