using Autofac;

namespace TMA.Data.Common
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterDataContext(this ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .SingleInstance()
                ;
            return builder;
        }
    }
}
