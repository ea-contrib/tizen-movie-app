using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMA.ServiceBase;

namespace TMA.PrincipalService
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(HostBuilderContext context, IServiceCollection collection, IConfiguration configuration)
        {
            base.ConfigureServices(context, collection, configuration);
            collection.AddHostedService<PrincipalServiceWorker>();
        }
    }
}