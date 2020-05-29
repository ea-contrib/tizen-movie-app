using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace TMA.Web.Common
{
    public static class WebServiceHoster
    {
        public static async Task RunAsync<TStartup>(string serviceName, string[] args) where TStartup : class
        {
            var factory = NLogBuilder.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["serviceName"] = serviceName;
            var logger = factory.GetLogger(serviceName);

            try
            {
                await CreateHostBuilder<TStartup>(args).Build().RunAsync();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, $"Stopped service {serviceName} because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : class
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Path.Combine(AppContext.BaseDirectory));
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(opts => opts.ListenAnyIP(80));

                    webBuilder.UseStartup<TStartup>();
                })
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseNLog();
        }
    }
}
