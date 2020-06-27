using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMA.ServiceBase;

namespace TMA.MovieService
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return ServiceHoster.RunAsync<Startup>("TMA.PrincipalService", args);
        }
    }
}
