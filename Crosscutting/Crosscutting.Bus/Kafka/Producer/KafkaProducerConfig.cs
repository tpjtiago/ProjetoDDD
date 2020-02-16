﻿namespace Crosscutting.Bus
{
    public class KafkaProducerConfig
    {
        public string BootstrapServers { get; set; }
        public string SaslUsername { get; set; }
        public string SaslPassword { get; set; }
        public string ClientId { get; set; }
        public string MessageTimeoutMs { get; set; }
    }
}