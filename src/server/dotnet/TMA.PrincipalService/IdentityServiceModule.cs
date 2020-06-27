using Autofac;
using TMA.MessageBus;

namespace TMA.PrincipalService
{
    public class IdentityServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterServiceBus();

            builder.RegisterType<PrincipalBlo>()
                .SingleInstance();
        }
    }
}
