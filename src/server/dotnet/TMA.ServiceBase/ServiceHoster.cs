using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NLog;

namespace PPL.Hoster
{
    public static class ServiceHoster
    {
        public static async Task RunAsync<TStartup>(string serviceName, string[] args)
            where TStartup : StartupBase, new()
        {
            var factory = LogManager.LoadConfiguration("nlog.config");
            LogManager.Configuration.Variables["serviceName"] = serviceName;
            var logger = factory.GetLogger(serviceName);
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                Directory.SetCurrentDirectory(path);
                var builder = new HostBuilder<TStartup>(serviceName);
                var host = await builder.ConfigureAndBuildAsync(args);
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Fatal(exception, $"Stopped service {serviceName} because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
