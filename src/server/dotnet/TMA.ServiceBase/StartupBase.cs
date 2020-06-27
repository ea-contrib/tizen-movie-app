using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMA.Migration;

namespace TMA.ServiceBase
{
    public class StartupBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupBase"/> class.
        /// </summary>
        public StartupBase()
        { }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="hostBuilderContext">The context.</param>
        /// <param name="builder">The builder.</param>
        public virtual void ConfigureContainer(HostBuilderContext hostBuilderContext, ContainerBuilder builder)
        {
            var assembliesInAppDomain = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            builder.RegisterAssemblyModules(assembliesInAppDomain);

            builder.RegisterMigrator();

        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="configuration"></param>
        public virtual void ConfigureServices(HostBuilderContext context, IServiceCollection collection,
            IConfiguration configuration)
        { }

        public virtual async Task PrepareEnvironmentAsync(IServiceProvider serviceProvider)
        {
            var migrator = serviceProvider.GetService<DbMigrator>();

            await migrator.MigrateUp();
        }
    }
}