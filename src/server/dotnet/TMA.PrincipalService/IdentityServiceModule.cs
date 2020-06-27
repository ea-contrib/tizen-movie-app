using Autofac;
using TMA.Data.Common;
using TMA.MessageBus;
using TMA.PrincipalService.Logic;
using TMA.PrincipalService.Repositories;

namespace TMA.PrincipalService
{
    public class IdentityServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterServiceBus();

            builder.RegisterDataContext();

            builder.RegisterType<PrincipalBlo>()
                .SingleInstance();

            builder.RegisterType<PrincipalRepository>()
                .SingleInstance();

            builder.RegisterType<PasswordHasher>()
                .SingleInstance();
        }
    }
}
