using System.Threading.Tasks;
using TMA.ServiceBase;

namespace TMA.BackgroundTaskService
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return ServiceHoster.RunAsync<Startup>("TMA.BackgroundTaskService", args);
        }
    }
}
