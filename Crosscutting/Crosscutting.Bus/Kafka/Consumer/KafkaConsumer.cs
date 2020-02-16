using Confluent.Kafka;
using Crosscutting.Domain.Bus;
using Crosscutting.Domain.Bus.Extensions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Bus
{
    public class KafkaConsumer : IKafkaConsumer
    {
        IConsumer<Ignore, string> _consumer;

        public KafkaConsumer(IConsumer<Ignore, string> consumer)
        {
            this._consumer = consumer;
        }

        public async Task<TResult<T>> Consume<T>(string topic)
        {
            TResult<T> resposta = new TResult<T>();
            try
            {
                Console.WriteLine($"Estou tentando ler o topic {topic}");

                _consumer.Subscribe(topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                var cr = _consumer.Consume(cts.Token);
                _consumer.Close();

                Console.WriteLine($"Topic lido com sucesso {cr?.Value?.ToString()}");

                resposta.Result = JsonSerializer.Deserialize<T>(cr.Value);
                resposta.Success = true;

                return resposta;
            }
            catch (ConsumeException ex)
            {
                resposta.Success = false;
                resposta.ErrorMessages = new List<string>() { ex.Message };

                Console.WriteLine($"Erro ao ler o topic {topic}");

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Success = false;
                resposta.ErrorMessages = new List<string>() { ex.Message };

                Console.WriteLine($"Erro ao ler o topic {topic}");

                return resposta;
            }

        }
    }
}