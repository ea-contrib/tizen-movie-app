using Autofac;
using TMA.Data.Common;
using TMA.MessageBus;
using TMA.MovieService.Clients.PuzzleMovies;
using TMA.MovieService.Logic;
using TMA.MovieService.Mappings;
using TMA.MovieService.Repositories;
using TMA.ServiceBase;

namespace TMA.MovieService
{
    public class MovieServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterDataContext();
            builder.RegisterServiceBus();
            builder.RegisterAutomapper();
            builder.RegisterMapping<MovieProfile>();

            builder.RegisterType<PuzzleMoviesClientConfigurationFactory>()
                .SingleInstance();

            builder.RegisterType<PuzzleMoviesClient>()
                .SingleInstance();

            builder.RegisterType<MovieBlo>()
                .SingleInstance();

            builder.RegisterType<MovieRepository>()
                .SingleInstance();


            builder.RegisterType<SaveChangesHandler>()
                .As<IPostExecuteHandler>()
                .SingleInstance();
        }
    }
}
