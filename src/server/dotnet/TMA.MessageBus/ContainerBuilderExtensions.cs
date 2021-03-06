﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TMA.Configuration;

namespace TMA.MessageBus
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterServiceBus(this ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var config = x.Resolve<IConfiguration>();

                var connectionString = config.GetRabbitMQConnectionString();

                return new RabbitMQMessageBus(
                    new RabbitMQMessageBusOptions(connectionString), 
                    x.Resolve<ILogger<RabbitMQMessageBus>>(), 
                    x.Resolve<IEnumerable<IErrorHandler>>().ToList(),
                    x.Resolve<IEnumerable<IPostExecuteHandler>>().ToList()
                    );
            })
                .As<IMessageBus>()
                .SingleInstance();

            return builder;
        }
    }
}
