using System;
using System.Text.RegularExpressions;

namespace TMA.MessageBus
{
    internal class ConnectionStringParams
    {
        private static readonly Regex ConnectionStringRegex = new Regex(@"amqp://(?<user>.*?):(?<password>.*?)@(?<host>.*?):(?<port>\d*?)/(?<virtualhost>.*)", RegexOptions.Compiled);

        public ConnectionStringParams(string hostName, string virtualHost, string userName, string password, int port)
        {
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
            Port = port;
        }
        public ConnectionStringParams(string connectionString)
        {
            var match = ConnectionStringRegex.Match(connectionString);

            if (!match.Success)
            {
                throw new ArgumentException("Invalid rabbitmq connection string");
            }

            UserName = match.Groups["user"].Value;
            Password = match.Groups["password"].Value;
            HostName = match.Groups["host"].Value;
            Port = int.Parse(match.Groups["port"].Value);
            VirtualHost = match.Groups["virtualhost"].Value;
        }

        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public static ConnectionStringParams FromString(string connectionString)
        {
            return new ConnectionStringParams(connectionString);
        }
    }
}