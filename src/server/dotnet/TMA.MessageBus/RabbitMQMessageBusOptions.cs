using System;

namespace TMA.MessageBus
{
    public class RabbitMQMessageBusOptions
    {
        public RabbitMQMessageBusOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
        public bool AutomaticRecoveryEnabled { get; set; } = true;
        public bool DispatchConsumersAsync { get; set; } = true;
        public bool UseBackgroundThreadsForIO { get; set; } = true;
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromMinutes(1);
        public IMessageSerializer MessageSerializer { get; set; } = new JsonMessageSerializer();
    }
}
