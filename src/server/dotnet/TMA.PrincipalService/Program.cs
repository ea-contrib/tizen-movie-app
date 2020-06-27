using System.Threading.Tasks;
using TMA.ServiceBase;

namespace TMA.PrincipalService
{
    public static class Program
    {
        private static Task Main(string[] args)
        {
            return ServiceHoster.RunAsync<Startup>("TMA.PrincipalService", args);
        }
    }
}