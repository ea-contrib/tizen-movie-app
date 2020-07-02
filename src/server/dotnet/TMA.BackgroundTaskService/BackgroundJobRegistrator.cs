using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using TMA.BackgroundTaskService.Jobs;
using TMA.Contracts.Messages;

namespace TMA.BackgroundTaskService
{
    public class BackgroundJobRegistrator
    {
        public void Register()
        {
            RecurringJob.AddOrUpdate<SynchronizePuzzleMoviesJob>(nameof(SynchronizePuzzleMoviesJob), x => x.Execute(), Cron.Minutely);
        }
    }
}
