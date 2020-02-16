using Confluent.Kafka;
using Crosscutting.Domain.Bus;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crosscutting.Bus
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(
            IProducer<Null, string> producer)
        {
            this._producer = producer;
        }

        public async Task Produce<T>(T message, string topic)
        {
            Console.WriteLine($"Criando o producer no topic {topic}");

            var id = (Guid)message.GetType().GetProperty("Id").GetValue(message, null);

            Console.WriteLine($"GUID: {id} na geração do producer no topic {topic}");

            Action<DeliveryReport<Null, string>> handler = r => Callback(r, id);

            try
            {
                string jsonData = JsonSerializer.Serialize<T>(message);
                Console.WriteLine($"Serialize da mensagem com sucesso");

                _producer.Produce(topic, new Message<Null, string> { Value = jsonData }, handler);
                Console.WriteLine($"Mensagem enviada com sucesso");

                _producer.Flush(TimeSpan.FromSeconds(10));
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return;
        }

        private void Callback(DeliveryReport<Null, string> handler, Guid id)
        {

            if (!handler.Error.IsError)
            {
                Console.WriteLine($"Sucesso na marcação do evento como processado do producer");
            }
            else
            {
                Console.WriteLine($"Erro na marcação do evento como processado do producer, error: ", JsonSerializer.Serialize(handler.Error).ToString());
                Console.WriteLine($"Detalhamento do erro: ", JsonSerializer.Serialize(handler).ToString());
            }
        }
    }
}