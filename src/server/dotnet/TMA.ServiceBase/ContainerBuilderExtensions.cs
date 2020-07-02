using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutoMapper;

namespace TMA.ServiceBase
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMapping<TProfile>(this ContainerBuilder builder) where TProfile : Profile
        {
            builder.RegisterType<TProfile>().As<Profile>().SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterAutomapper(this ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in ctx.Resolve<IList<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
            return builder;
        }
    }
}
