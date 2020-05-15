using Autofac;
using TMA.IdentityService.Handlers;
using TMA.IdentityService.Logic;
using TMA.IdentityService.ResponseHandling;

namespace TMA.IdentityService
{
    public class IdentityServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UserStore>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ClientStore>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance()
                .OnActivated(x => x.Instance.InitStore())
                .AutoActivate();

            builder.RegisterType<UserProfileService>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<PersistedGrantStore>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ResourceStore>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<CustomAuthorizeRequestValidator>()
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<CustomIntrospectionResponseGenerator>()
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterType<CorsPolicyService>()
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterType<UserResourceOwnerPasswordValidator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
