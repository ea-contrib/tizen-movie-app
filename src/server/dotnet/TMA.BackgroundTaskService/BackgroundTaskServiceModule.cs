using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using TMA.BackgroundTaskService.Jobs;
using TMA.Common.Interfaces;
using TMA.MessageBus;

namespace TMA.BackgroundTaskService
{
    public class BackgroundTaskServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterServiceBus();

            builder.RegisterType<SynchronizePuzzleMoviesJob>()
                .As<IBackgroundJob>()
                .SingleInstance();


            builder.RegisterType<BackgroundJobRegistrator>().SingleInstance()
                .OnActivated(x => x.Instance.Register())
                .AutoActivate();
        }
    }
}
