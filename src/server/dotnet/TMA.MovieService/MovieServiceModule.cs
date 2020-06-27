using Autofac;
using TMA.MessageBus;

namespace TMA.MovieService
{
    public class MovieServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterServiceBus();

            builder.RegisterType<MovieBlo>()
                .SingleInstance();
        }
    }
}
