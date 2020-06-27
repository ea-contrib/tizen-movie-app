using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TMA.Configuration
{
    public static class ConfigurationExtensions
    {
        public static string GetRabbitMQConnectionString(this IConfiguration configuration)
        {
            return configuration["MessageBus:ConnectionString"];
        }

        public static string IdpUrl(this IConfiguration configuration)
        {
            return configuration["IdentityServer:Audience"];
        }

        //__________________________________________________________________

        public static string GetDbConnectionString(this IConfiguration configuration, string name)
        {
            return configuration[$"connection-strings:{name}"];
        }
       
        public static string PostmanClientSecret(this IConfiguration configuration)
        {
            return "test";
        }
        public static string AppUrl(this IConfiguration configuration)
        {
            return "test";
        }
        
        public static int SessionExpirationInMinutes(this IConfiguration configuration)
        {
            return 60;
        }
        public static string ApiSecret(this IConfiguration configuration)
        {
            return "test";
        }
        public static bool IsPostmanClientEnabled(this IConfiguration configuration)
        {
            return true;
        }
        public static List<string> AllowedHosts(this IConfiguration configuration)
        {
            return new List<string>();
        }
    }
}
