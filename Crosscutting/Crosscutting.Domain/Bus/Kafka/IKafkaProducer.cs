using System.Threading.Tasks;

namespace Crosscutting.Domain.Bus
{
    public interface IKafkaProducer
    {
        Task Produce<T>(T theEvent, string topic);
    }
}