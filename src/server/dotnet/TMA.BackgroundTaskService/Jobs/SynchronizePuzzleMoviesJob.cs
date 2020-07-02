using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMA.Contracts.Messages;
using TMA.MessageBus;

namespace TMA.BackgroundTaskService.Jobs
{
    public class SynchronizePuzzleMoviesJob: IBackgroundJob
    {
        private readonly IMessageBus _messageBus;

        public SynchronizePuzzleMoviesJob(IMessageBus messageBus)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        public async Task Execute()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(10));
            await _messageBus.PublishAsync<SynchronizePuzzleMoviesCommand, ResponseMessage>(new SynchronizePuzzleMoviesCommand(), cts.Token);
        }
    }
}
