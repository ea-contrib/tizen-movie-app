using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace TMA.ServiceBase
{
    public class HostBuilder<T> : HostBuilder
        where T : StartupBase, new()
    {
        private readonly string _serviceName;
        private readonly StartupBase _startup;

        public HostBuilder(string serviceName)
        {
            _serviceName = serviceName;
            _startup = new T();
        }

        public async Task<IHost> ConfigureAndBuildAsync(string[] args)
        {
            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((context, collection) => _startup.ConfigureServices(context, collection, context.Configuration))
                .ConfigureContainer<ContainerBuilder>((hostBuilderContext, containerBuilder) => _startup.ConfigureContainer(hostBuilderContext, containerBuilder))
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddEnvironmentVariables(prefix: "DOTNETCORE_");
                    Console.Title = _serviceName;
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = hostContext.HostingEnvironment;
                    config.SetBasePath(Path.Combine(AppContext.BaseDirectory))
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureLogging((_, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddNLog();
                })
                .UseConsoleLifetime()
                .Build();

            await _startup.PrepareEnvironmentAsync(host.Services);

            return host;
        }
    }
}