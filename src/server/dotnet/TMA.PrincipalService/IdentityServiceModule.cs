using System.Collections.Generic;
using Autofac;
using AutoMapper;
using TMA.Data.Common;
using TMA.MessageBus;
using TMA.PrincipalService.Logic;
using TMA.PrincipalService.Mappings;
using TMA.PrincipalService.Repositories;
using TMA.ServiceBase;

namespace TMA.PrincipalService
{
    public class IdentityServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterServiceBus();

            builder.RegisterDataContext();

            builder.RegisterAutomapper();

            builder.RegisterMapping<GrantProfile>();


            builder.RegisterType<PrincipalBlo>()
                .SingleInstance();

            builder.RegisterType<GrantBlo>()
                .SingleInstance();

            builder.RegisterType<PrincipalRepository>()
                .SingleInstance();

            builder.RegisterType<GrantRepository>()
                .SingleInstance();

            builder.RegisterType<PasswordHasher>()
                .SingleInstance();



            builder.RegisterType<SaveChangesHandler>()
                .As<IPostExecuteHandler>()
                .SingleInstance();
        }
    }
}
