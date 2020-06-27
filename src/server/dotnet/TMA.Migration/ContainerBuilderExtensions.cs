using Autofac;

namespace TMA.Migration
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMigrator(this ContainerBuilder builder)
        {
            builder.RegisterType<DbMigrator>()
                .SingleInstance();
            return builder;
        }
    }
}
