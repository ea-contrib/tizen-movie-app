using System.Threading.Tasks;
using PPL.Hoster;

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