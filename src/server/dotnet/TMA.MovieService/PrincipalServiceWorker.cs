using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TMA.MessageBus;

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
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public void Dispose()
        {
        }
    }
}
