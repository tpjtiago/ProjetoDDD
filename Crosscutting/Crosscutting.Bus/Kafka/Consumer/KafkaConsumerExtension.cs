using Confluent.Kafka;
using Crosscutting.Domain.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crosscutting.Bus
{
    public static class KafkaConsumerExtension
    {
        public static void AddKafkaConsumer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new KafkaConsumerConfig();

            configuration.Bind("KafkaConsumer", config);

            if (string.IsNullOrEmpty(config.BootstrapServers))
                throw new Exception("Kafka Consumer Servers connection is empty.");

            services.AddTransient<IKafkaConsumer>(s => new KafkaConsumer(new ConsumerBuilder<Ignore, string>(new ConsumerConfig
            {
                GroupId = config.GroupId,
                BootstrapServers = config.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)Enum.Parse(typeof(AutoOffsetReset), config.AutoOffsetReset),
                EnableAutoCommit = Convert.ToBoolean(config.EnableAutoCommit),
                ClientId = config.ClientId,
                //SecurityProtocol = SecurityProtocol.SaslSsl,
                //SaslMechanism = (SaslMechanism)Enum.Parse(typeof(SaslMechanism), config.SaslMechanism),
                //SaslUsername = config.SaslUsername,
                //SaslPassword = config.SaslPassword
            }).Build()));
        }
    }
}