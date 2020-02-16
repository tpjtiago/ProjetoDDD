using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crosscutting.Bus
{
    public static class KafkaProducerExtension
    {
        public static void AddKafkaProducer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new KafkaProducerConfig();

            configuration.Bind("KafkaProducer", config);

            if (string.IsNullOrEmpty(config.BootstrapServers))
                throw new Exception("Kafka Producer Servers connection is empty.");

            services.AddScoped<IProducer<Null, string>>(s => new ProducerBuilder<Null, string>(new ProducerConfig
            {
                BootstrapServers = config.BootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = config.SaslUsername,
                SaslPassword = config.SaslPassword,
                ClientId = config.ClientId,
                MessageTimeoutMs = Convert.ToInt32(config.MessageTimeoutMs)

            }).Build());
        }
    }
}