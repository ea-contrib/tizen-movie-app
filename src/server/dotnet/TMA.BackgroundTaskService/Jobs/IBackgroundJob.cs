using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMA.BackgroundTaskService.Jobs
{
    public interface IBackgroundJob
    {
        Task Execute();
    }
}
