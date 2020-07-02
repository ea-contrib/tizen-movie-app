using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;
using TMA.MovieService.Logic;

namespace TMA.MovieService
{
    public class MovieServiceWorker: IHostedService, IDisposable
    {
        private readonly IMessageBus _messageBus;
        private readonly MovieBlo _blo;

        public MovieServiceWorker(IMessageBus messageBus, MovieBlo blo)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _blo = blo ?? throw new ArgumentNullException(nameof(blo));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        { 
            await _messageBus.SubscribeAsync<SynchronizePuzzleMoviesCommand, ResponseMessage>(x => _blo.SynchronizePuzzleMovies());
            await _messageBus.SubscribeAsync<GetMoviesCommand, ResponseMessage<List<MovieDto>>>(x => _blo.List());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public void Dispose()
        {
        }
    }
}
