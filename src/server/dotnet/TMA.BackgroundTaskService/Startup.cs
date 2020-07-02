using System;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMA.Configuration;
using TMA.ServiceBase;

namespace TMA.BackgroundTaskService
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(HostBuilderContext context, IServiceCollection collection, IConfiguration configuration)
        {
            base.ConfigureServices(context, collection, configuration);

            GlobalConfiguration.Configuration.UsePostgreSqlStorage(configuration.GetDbConnectionString(), new PostgreSqlStorageOptions()
            {
                QueuePollInterval = TimeSpan.FromSeconds(20),
            });

            collection.AddHangfire(_ => {});

            collection.AddHangfireServer(x => x.WorkerCount = 1);
        }
    }
}