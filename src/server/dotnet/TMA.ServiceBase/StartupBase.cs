using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PPL.Hoster
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

        public virtual Task PrepareEnvironmentAsync(IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }
    }
}