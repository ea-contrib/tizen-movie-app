using System;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMA.Configuration;

namespace TMA.Migration
{
    public class DbMigrator
    {
        private readonly IConfiguration _configuration;

        public DbMigrator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task MigrateUp()
        {
            var serviceProvider = CreateServices();

            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider);
                }
            }
            catch (MissingMigrationsException)
            {
            }
           

            return Task.CompletedTask;
        }

        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(_configuration.GetDbConnectionString())
                    .ScanIn(AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.Contains("TMA")).ToArray()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
